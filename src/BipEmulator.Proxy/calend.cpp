/*
	(С) Волков Максим 2019 ( Maxim.N.Volkov@ya.ru )
	
	Календарь v1.0  
	Приложение простого календаря.
	Алгоритм вычисления дня недели работает для любой даты григорианского календаря позднее 1583 года. 
	Григорианский календарь начал действовать в 1582 — после 4 октября сразу настало 15 октября. 
	
	Календарь от 1600 до 3000 года
	Функции перелистывания каленаря вверх-вниз - месяц, стрелками год
	При нажатии на название месяца устанавливается текущая дата
	
	
	v.1.1
	- исправлены переходы в при запуске из бстрого меню
	
*/

#include "libbip.h"
#include "calend.h"
#define DEBUG_LOG

//	структура меню экрана календаря
struct regmenu_ menu_calend_screen = {
						55,
						1,
						0,
						dispatch_calend_screen,
						key_press_calend_screen, 
						calend_screen_job,
						0,
						show_calend_screen,
						0,
						0
					};

int main(int param0, char** argv){	//	переменная argv не определена
	show_calend_screen((void*) param0);
}

void show_calend_screen (void *param0){
struct calend_** 	calend_p = (calend_ **)get_ptr_temp_buf_2(); 	//	указатель на указатель на данные экрана 
struct calend_ *	calend;								//	указатель на данные экрана
struct calend_opt_ 	calend_opt;							//	опции календаря

#ifdef DEBUG_LOG
log_printf(5, "[show_calend_screen] param0=%X; *temp_buf_2=%X; menu_overlay=%d", (int)param0, (int*)get_ptr_temp_buf_2(), get_var_menu_overlay());
log_printf(5, " #calend_p=%X; *calend_p=%X", (int)calend_p, (int)*calend_p);
#endif

if ( (param0 == *calend_p) && get_var_menu_overlay()){ // возврат из оверлейного экрана (входящий звонок, уведомление, будильник, цель и т.д.)

#ifdef DEBUG_LOG
	log_printf(5, "  #from overlay");
	log_printf(5, "\r\n");
#endif	

	calend = *calend_p;						//	указатель на данные необходимо сохранить для исключения 
											//	высвобождения памяти функцией reg_menu
	*calend_p = (calend_*)NULL;						//	обнуляем указатель для передачи в функцию reg_menu	

	// 	создаем новый экран, при этом указатели temp_buf_1 и temp_buf_2 были равны 0 и память не была высвобождена	
	reg_menu(&menu_calend_screen, 0); 		// 	menu_overlay=0
	
	*calend_p = calend;						//	восстанавливаем указатель на данные после функции reg_menu
	
	draw_month(0, calend->month, calend->year);
	
} else { 			// если запуск функции произошел из меню, 

#ifdef DEBUG_LOG
	log_printf(5, "  #from menu");
	log_printf(5, "\r\n");
#endif
	// создаем экран
	reg_menu(&menu_calend_screen, 0);

	// выделяем необходимую память и размещаем в ней данные
	*calend_p = (struct calend_ *)pvPortMalloc(sizeof(struct calend_));
	calend = *calend_p;		//	указатель на данные
	
	// очистим память под данные
	_memclr(calend, sizeof(struct calend_));
	
	calend->proc = (Elf_proc_*)param0;
	
	// запомним адрес указателя на функцию в которую необходимо вернуться после завершения данного экрана
	if ( param0 && calend->proc->elf_finish ) 			//	если указатель на возврат передан, то возвоащаемся на него
		calend->ret_f = calend->proc->elf_finish;
	else					//	если нет, то на циферблат
		calend->ret_f = show_watchface;
	
	struct datetime_ datetime;
	_memclr(&datetime, sizeof(struct datetime_));
	
		// получим текущую дату
	get_current_date_time(&datetime);
	
	calend->day 	= datetime.day;
	calend->month 	= datetime.month;
	calend->year 	= datetime.year;

	// считаем опции из flash памяти, если значение в флэш-памяти некорректное то берем первую схему
	// текущая цветовая схема хранится о смещению 0
	ElfReadSettings(calend->proc->index_listed, &calend_opt, OPT_OFFSET_CALEND_OPT, sizeof(struct calend_opt_));
	
	if (calend_opt.color_scheme < COLOR_SCHEME_COUNT) 
			calend->color_scheme = calend_opt.color_scheme;
	else 
			calend->color_scheme = 0;

	draw_month(calend->day, calend->month, calend->year);
}

// при бездействии погасить подсветку и не выходить
set_display_state_value(8, 1);
set_display_state_value(2, 1);

// таймер на job на 20с где выход.
set_update_period(1, INACTIVITY_PERIOD);

}

void draw_month(unsigned int day, unsigned int month, unsigned int year){
struct calend_** 	calend_p = (calend_ **)get_ptr_temp_buf_2(); 		//	указатель на указатель на данные экрана 
struct calend_ *	calend = *calend_p;						//	указатель на данные экрана


/*
 0:	CALEND_COLOR_BG					фон календаря
 1:	CALEND_COLOR_MONTH				цвет названия текущего месяца
 2:	CALEND_COLOR_YEAR				цвет текущего года
 3:	CALEND_COLOR_WORK_NAME			цвет названий дней будни
 4: CALEND_COLOR_HOLY_NAME_BG		фон	 названий дней выходные
 5:	CALEND_COLOR_HOLY_NAME_FG		цвет названий дней выходные
 6:	CALEND_COLOR_SEPAR				цвет разделителей календаря
 7:	CALEND_COLOR_NOT_CUR_WORK		цвет чисел НЕ текущего месяца будни
 8:	CALEND_COLOR_NOT_CUR_HOLY_BG	фон  чисел НЕ текущего месяца выходные
 9:	CALEND_COLOR_NOT_CUR_HOLY_FG	цвет чисел НЕ текущего месяца выходные
10:	CALEND_COLOR_CUR_WORK			цвет чисел текущего месяца будни
11:	CALEND_COLOR_CUR_HOLY_BG		фон  чисел текущего месяца выходные
12:	CALEND_COLOR_CUR_HOLY_FG		цвет чисел текущего месяца выходные
13: CALEND_COLOR_TODAY_BG			фон  чисел текущего дня; 		bit 31 - заливка: =0 заливка цветом фона, =1 только рамка, фон как у числа не текущего месяца 
14: CALEND_COLOR_TODAY_FG			цвет чисел текущего дня
*/


static unsigned char short_color_scheme[COLOR_SCHEME_COUNT][15] = 	
/* черная тема без выделения выходных*/		{//		0				1			2			3			4			5			6
											 {COLOR_SH_BLACK, COLOR_SH_YELLOW, COLOR_SH_AQUA, COLOR_SH_WHITE, COLOR_SH_RED, COLOR_SH_WHITE, COLOR_SH_WHITE, 
											 COLOR_SH_GREEN, COLOR_SH_BLACK, COLOR_SH_AQUA, COLOR_SH_YELLOW, COLOR_SH_BLACK, COLOR_SH_WHITE, COLOR_SH_YELLOW, COLOR_SH_BLACK}, 
											 //		7				8			9			10			11			12			13			14 
											 //		0				1			2			3			4			5			6
/* белая тема без выделения выходных*/		{COLOR_SH_WHITE, COLOR_SH_BLACK, COLOR_SH_BLUE, COLOR_SH_BLACK, COLOR_SH_RED, COLOR_SH_WHITE, COLOR_SH_BLACK, 
											 COLOR_SH_BLUE, COLOR_SH_WHITE, COLOR_SH_AQUA, COLOR_SH_BLACK, COLOR_SH_WHITE, COLOR_SH_RED, COLOR_SH_BLUE, COLOR_SH_WHITE},
											 //		7				8			9			10			11			12			13			14 	 
											 //		0				1			2			3			4			5			6
/* черная тема с выделением выходных*/		{COLOR_SH_BLACK, COLOR_SH_YELLOW, COLOR_SH_AQUA, COLOR_SH_WHITE, COLOR_SH_RED, COLOR_SH_WHITE, COLOR_SH_WHITE, 
											 COLOR_SH_GREEN, COLOR_SH_RED, COLOR_SH_AQUA, COLOR_SH_YELLOW, COLOR_SH_RED, COLOR_SH_WHITE, COLOR_SH_AQUA, COLOR_SH_BLACK}, 
											 //		7				8			9			10			11			12 			13			14 
											 //		0				1			2			3			4			5			6
/* белая тема с выделением выходных*/		{COLOR_SH_WHITE, COLOR_SH_BLACK, COLOR_SH_BLUE, COLOR_SH_BLACK, COLOR_SH_RED, COLOR_SH_WHITE, COLOR_SH_BLACK, 
											 COLOR_SH_BLUE, COLOR_SH_RED, COLOR_SH_BLUE, COLOR_SH_BLACK, COLOR_SH_RED, COLOR_SH_BLACK, COLOR_SH_BLUE, COLOR_SH_WHITE},
											 //		7				8			9			10			11			12			13			14 	 
											//		0				1			2			3			4			5			6
/* черная тема без выделения выходных*/		{COLOR_SH_BLACK, COLOR_SH_YELLOW, COLOR_SH_AQUA, COLOR_SH_WHITE, COLOR_SH_RED, COLOR_SH_WHITE, COLOR_SH_WHITE, 
/*с рамкой выделения сегодняшнего дня*/	     COLOR_SH_GREEN, COLOR_SH_BLACK, COLOR_SH_AQUA, COLOR_SH_YELLOW, COLOR_SH_BLACK, COLOR_SH_WHITE, COLOR_SH_AQUA|(1<<7), COLOR_SH_BLACK},
											 //		7				8			9			10			11			12			13			14 	 
											
											};

int color_scheme[COLOR_SCHEME_COUNT][15];


for (unsigned char i=0;i<COLOR_SCHEME_COUNT;i++)
	for (unsigned char j=0;j<15;j++){
	color_scheme[i][j]  = (((unsigned int)short_color_scheme[i][j]&(unsigned char)COLOR_SH_MASK)&COLOR_SH_RED)  ?COLOR_RED   :0;	//	составляющая красного цвета
	color_scheme[i][j] |= (((unsigned int)short_color_scheme[i][j]&(unsigned char)COLOR_SH_MASK)&COLOR_SH_GREEN)?COLOR_GREEN :0;	//	составляющая зеленого цвета
	color_scheme[i][j] |= (((unsigned int)short_color_scheme[i][j]&(unsigned char)COLOR_SH_MASK)&COLOR_SH_BLUE) ?COLOR_BLUE  :0;	//	составляющая синего цвета
	color_scheme[i][j] |= (((unsigned int)short_color_scheme[i][j]&(unsigned char)(1<<7))) ?(1<<31) :0;				//	для рамки	
}

char text_buffer[24];
char *weekday_string_ru[] = {"??", "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс"};
char *weekday_string_en[] = {"??", "Mo", "Tu", "We", "Th", "Fr", "Sa", "Su"};
char *weekday_string_it[] = {"??", "Lu", "Ma", "Me", "Gi", "Ve", "Sa", "Do"};
char *weekday_string_fr[] = {"??", "Lu", "Ma", "Me", "Je", "Ve", "Sa", "Di"};
char *weekday_string_es[] = {"??", "Lu", "Ma", "Mi", "Ju", "Vi", "Sá", "Do"};

char *weekday_string_short_ru[] = {"?", "П", "В", "С", "Ч", "П", "С", "В"};
char *weekday_string_short_en[] = {"?", "M", "T", "W", "T", "F", "S", "S"};
char *weekday_string_short_it[] = {"?", "L", "M", "M", "G", "V", "S", "D"};
char *weekday_string_short_fr[] = {"?", "M", "T", "W", "T", "F", "S", "S"};
char *weekday_string_short_es[] = {"?", "L", "M", "X", "J", "V", "S", "D"};

char *monthname_ru[] = {
	     "???",		
	     "Январь", 		"Февраль", 	"Март", 	"Апрель",
		 "Май", 		"Июнь",		"Июль", 	"Август", 
		 "Сентябрь", 	"Октябрь", 	"Ноябрь",	"Декабрь"};

char *monthname_en[] = {
	     "???",		
	     "January", 	"February", 	"March", 		"April", 	
		 "May", 		"June",			"July", 		"August",
		 "September", 	"October", 		"November",		"December"};

char *monthname_it[] = {
	     "???",		
	     "Gennaio", 	"Febbraio", "Marzo", 	"Aprile", 
		 "Maggio", 		"Giugno",   "Luglio", 	"Agosto", 
		 "Settembre", 	"Ottobre", 	"Novembre", "Dicembre"};
		 
char *monthname_fr[] = {
	     "???",		
	     "Janvier",		"Février",	"Mars",		"Avril", 
		 "Mai", 		"Juin", 	"Juillet",	"Août", 
		 "Septembre", 	"Octobre", 	"Novembre", "Décembre"};
char *monthname_es[] = {
	     "???",		
	     "Enero", 		"Febrero", 	"Marzo", 		"Abril", 	
		 "Mayo", 		"Junio", 	"Julio",		"Agosto", 	
		 "Septiembre", 	"Octubre", 	"Noviembre", 	"Diciembre"};


unsigned char day_month[] = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

char**	weekday_string;
char**	weekday_string_short;
char**	monthname;

switch (get_selected_locale()){
		case locale_ru_RU:{
			weekday_string = weekday_string_ru;
			weekday_string_short = weekday_string_short_ru;
			monthname = monthname_ru;
			break;
		}
		case locale_it_IT:{
			weekday_string = weekday_string_it;
			weekday_string_short = weekday_string_short_it;
			monthname = monthname_it;
			break;
		}
		case locale_fr_FR:{
			weekday_string = weekday_string_fr;
			weekday_string_short = weekday_string_short_fr;
			monthname = monthname_fr;
			break;
		}
		case locale_es_ES:{
			weekday_string = weekday_string_es;
			weekday_string_short = weekday_string_short_es;
			monthname = monthname_es;
			break;
		}
		default:{
			weekday_string = weekday_string_en;
			weekday_string_short = weekday_string_short_en;
			monthname = monthname_en;
			break;
		}
	}

_memclr(&text_buffer, 24);

set_bg_color(color_scheme[calend->color_scheme][CALEND_COLOR_BG]);	//	фон календаря
fill_screen_bg();

set_graph_callback_to_ram_1();
load_font(); // подгружаем шрифты

_sprintf(&text_buffer[0], " %d", year);
int month_text_width = text_width(monthname[month]);
int year_text_width  = text_width(&text_buffer[0]);

set_fg_color(color_scheme[calend->color_scheme][CALEND_COLOR_MONTH]);		//	цвет месяца
text_out(monthname[month], (176-month_text_width-year_text_width)/2 ,5); 	// 	вывод названия месяца

set_fg_color(color_scheme[calend->color_scheme][CALEND_COLOR_YEAR]);		//	цвет года
text_out(&text_buffer[0],  (176+month_text_width-year_text_width)/2 ,5); 	// 	вывод года

text_out("←", 5		 ,5); 		// вывод стрелки влево
text_out("→", 176-5-text_width("→"),5); 		// вывод стрелки вправо

int calend_name_height = get_text_height();

set_fg_color(color_scheme[calend->color_scheme][CALEND_COLOR_SEPAR]);
draw_horizontal_line(CALEND_Y_BASE, H_MARGIN, 176-H_MARGIN);	// Верхний разделитель названий дней недели
draw_horizontal_line(CALEND_Y_BASE+1+V_MARGIN+calend_name_height+1+V_MARGIN, H_MARGIN, 176-H_MARGIN);	// Нижний  разделитель названий дней недели
//draw_horizontal_line(175, H_MARGIN, 176-H_MARGIN);	// Нижний  разделитель месяца
 
// Названия дней недели
for (unsigned i=1; (i<=7);i++){
	if ( i>5 ){		//	выходные
		set_bg_color(color_scheme[calend->color_scheme][CALEND_COLOR_HOLY_NAME_BG]);
		set_fg_color(color_scheme[calend->color_scheme][CALEND_COLOR_HOLY_NAME_FG]);
	} else {		//	рабочие
		set_bg_color(color_scheme[calend->color_scheme][CALEND_COLOR_BG]);	
		set_fg_color(color_scheme[calend->color_scheme][CALEND_COLOR_WORK_NAME]);
	}
	
	
	// отрисовка фона названий выходных    
	int pos_x1 = H_MARGIN +(i-1)*(WIDTH  + H_SPACE);
	int pos_y1 = CALEND_Y_BASE+V_MARGIN+1;
	int pos_x2 = pos_x1 + WIDTH;
	int pos_y2 = pos_y1 + calend_name_height;

	// фон для каждого названия дня недели
	draw_filled_rect_bg(pos_x1, pos_y1, pos_x2, pos_y2);
	
	// вывод названий дней недели. если ширина названия больше чем ширина поля, выводим короткие названия
	if (text_width(weekday_string[1]) <= (WIDTH - 2))
		text_out_center(weekday_string[i], pos_x1 + WIDTH/2, pos_y1 + (calend_name_height-get_text_height())/2 );	
	else 
		text_out_center(weekday_string_short[i], pos_x1 + WIDTH/2, pos_y1 + (calend_name_height-get_text_height())/2 );	
}


int calend_days_y_base = CALEND_Y_BASE+1+V_MARGIN+calend_name_height+V_MARGIN+1;

if (isLeapYear(year)>0) day_month[2]=29;

unsigned char d=wday(1,month, year);
unsigned char m=month;
if (d>1) {
     m=(month==1)?12:month-1;
     d=day_month[m]-d+2;
	}

// числа месяца
for (unsigned i=1; (i<=7*6);i++){
     
	 unsigned char row = (i-1)/7;
     unsigned char col = (i-1)%7+1;
         
    _sprintf (&text_buffer[0], "%2.0d", d);
	
	int bg_color = 0;
	int fg_color = 0;
	int frame_color = 0; 	// цветрамки
	int frame    = 0; 		// 1-рамка; 0 - заливка
	
	// если текущий день текущего месяца
	if ( (m==month)&&(d==day) ){
		
		if ( color_scheme[calend->color_scheme][CALEND_COLOR_TODAY_BG] & (1<<31) ) {// если заливка отключена  только рамка
			
			// цвет рамки устанавливаем CALEND_COLOR_TODAY_BG, фон внутри рамки и цвет текста такой же как был
			frame_color = (color_scheme[calend->color_scheme][CALEND_COLOR_TODAY_BG &COLOR_MASK]);
			// рисуем рамку
			frame = 1;
			
			if ( col > 5 ){ // если выходные 
				bg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_CUR_HOLY_BG]); 
				fg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_CUR_HOLY_FG]);
			} else {		//	если будни
				bg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_BG]); 
				fg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_CUR_WORK]);
			};
			
		} else { 						// если включена заливка	
			if ( col > 5 ){ // если выходные 
				bg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_TODAY_BG] & COLOR_MASK); 
				fg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_TODAY_FG]);
			} else {		//	если будни
				bg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_TODAY_BG] &COLOR_MASK); 
				fg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_TODAY_FG]);
			};
		};
		

	} else {
	if ( col > 5 ){  // если выходные 
		if (month == m){
			bg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_CUR_HOLY_BG]); 
			fg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_CUR_HOLY_FG]);
		} else {
			bg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_NOT_CUR_HOLY_BG]); 
			fg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_NOT_CUR_HOLY_FG]);
		}
	} else {		//	если будни
		if (month == m){
			bg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_BG]); 
			fg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_CUR_WORK]);
		} else {
			bg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_BG]); 
			fg_color = (color_scheme[calend->color_scheme][CALEND_COLOR_NOT_CUR_WORK]);
		}
	}
	}
	
	
	
	//  строка: от 7 до 169 = 162рх в ширину 7 чисел по 24рх на число 7+(22+2)*6+22+3
	//  строки: от 57 до 174 = 117рх в высоту 6 строк по 19рх на строку 1+(17+2)*5+18
	
	// отрисовка фона числа
	int pos_x1 = H_MARGIN +(col-1)*(WIDTH  + H_SPACE);
	int pos_y1 = calend_days_y_base + V_MARGIN + row *(HEIGHT + V_SPACE)+1;
	int pos_x2 = pos_x1 + WIDTH;
	int pos_y2 = pos_y1 + HEIGHT;	
	
	if (frame){
		// выводим число
		set_bg_color(bg_color);
		set_fg_color(fg_color);
		text_out_center(&text_buffer[0], pos_x1+WIDTH/2, pos_y1+(HEIGHT-get_text_height())/2);
		
		//	рисуем рамку
		set_fg_color(frame_color);
		draw_rect(pos_x1, pos_y1, pos_x2-1, pos_y2-1);	
	} else {
		// рисуем заливку
		set_bg_color(bg_color);
		draw_filled_rect_bg(pos_x1, pos_y1, pos_x2, pos_y2);
		
		// выводим число
		set_fg_color(fg_color);
		text_out_center(&text_buffer[0], pos_x1+WIDTH/2, pos_y1+(HEIGHT-get_text_height())/2);
	};
	
	
	
		
	if ( d < day_month[m] ) {
		d++;
	} else {
		d=1; 
		m=(m==12)?1:(m+1);
	}
}



};

unsigned char wday(unsigned int day,unsigned int month,unsigned int year)
{
    signed int a = (14 - month) / 12;
    signed int y = year - a;
    signed int m = month + 12 * a - 2;
    return 1+(((day + y + y / 4 - y / 100 + y / 400 + (31 * m) / 12) % 7) +6)%7;
}

unsigned char isLeapYear(unsigned int year){
    unsigned char result = 0;
    if ( (year % 4)   == 0 ) result++;
    if ( (year % 100) == 0 ) result--;
    if ( (year % 400) == 0 ) result++;
return result;
}

void key_press_calend_screen(){
	struct calend_** 	calend_p = (calend_ **)get_ptr_temp_buf_2(); 		//	указатель на указатель на данные экрана 
	struct calend_ *	calend = *calend_p;			//	указатель на данные экрана
	
	show_menu_animate(calend->ret_f, (unsigned int)show_calend_screen, ANIMATE_RIGHT);	
};


void calend_screen_job(){
	struct calend_** 	calend_p = (calend_ **)get_ptr_temp_buf_2(); 		//	указатель на указатель на данные экрана 
	struct calend_ *	calend = *calend_p;			//	указатель на данные экрана
	
	// при достижении таймера обновления выходим
	show_menu_animate(calend->ret_f, (unsigned int)show_calend_screen, ANIMATE_LEFT);
}

int dispatch_calend_screen (void *param){
	struct calend_** 	calend_p = (calend_ **)get_ptr_temp_buf_2(); 		//	указатель на указатель на данные экрана 
	struct calend_ *	calend = *calend_p;			//	указатель на данные экрана
	
	struct calend_opt_ calend_opt;					//	опции календаря
	
	struct datetime_ datetime;
	// получим текущую дату
	
		
	get_current_date_time(&datetime);
	unsigned int day;
	
	//	char text_buffer[32];	
	 struct gesture_ *gest = (gesture_*) param;
	 int result = 0;
	 
	switch (gest->gesture){
		case GESTURE_CLICK: {
			
			// вибрация при любом нажатии на экран
			vibrate (1, 40, 0);
			
			
			if ( gest->touch_pos_y < CALEND_Y_BASE ){ // кликнули по верхней строке
				if (gest->touch_pos_x < 44){
					if ( calend->year > 1600 ) calend->year--;
				} else 
				if (gest->touch_pos_x > (176-44)){
					if ( calend->year < 3000 ) calend->year++;
				} else {
					calend->day 	= datetime.day;
					calend->month 	= datetime.month;
					calend->year 	= datetime.year;
				}	

				 if ( (calend->year == datetime.year) && (calend->month == datetime.month) ){
					day = datetime.day;
				 } else {	
					day = 0;
				 }
					draw_month(day, calend->month, calend->year);
					repaint_screen_lines(1, 176);			
				
			} else { // кликнули в календарь
			
				calend->color_scheme = ((calend->color_scheme+1)%COLOR_SCHEME_COUNT);
						
				// сначала обновим экран
				if ( (calend->year == datetime.year) && (calend->month == datetime.month) ){
					day = datetime.day;
				 } else {	
					day = 0;
				 }
					draw_month(day, calend->month, calend->year);
					repaint_screen_lines(1, 176);			
					
				// потом запись опций во flash память, т.к. это долгая операция
				// TODO: 1. если опций будет больше чем цветовая схема - переделать сохранение, чтобы сохранять перед выходом.
				calend_opt.color_scheme = calend->color_scheme;	

				// запишем настройки в флэш память
				ElfWriteSettings(calend->proc->index_listed, &calend_opt, OPT_OFFSET_CALEND_OPT, sizeof(struct calend_opt_));
			}
			
			// продлить таймер выхода при бездействии через INACTIVITY_PERIOD с
			set_update_period(1, INACTIVITY_PERIOD);
			break;
		};
		
		case GESTURE_SWIPE_RIGHT: 	//	свайп направо
		case GESTURE_SWIPE_LEFT: {	// справа налево
	
			if ( get_left_side_menu_active()){
					set_update_period(0,0);
					
					void* show_f = get_ptr_show_menu_func();

					// запускаем dispatch_left_side_menu с параметром param в результате произойдет запуск соответствующего бокового экрана
					// при этом произойдет выгрузка данных текущего приложения и его деактивация.
					dispatch_left_side_menu((gesture_*)param);
										
					if ( get_ptr_show_menu_func() == show_f ){
						// если dispatch_left_side_menu отработал безуспешно (листать некуда) то в show_menu_func по прежнему будет 
						// содержаться наша функция show_calend_screen, тогда просто игнорируем этот жест
						
						// продлить таймер выхода при бездействии через INACTIVITY_PERIOD с
						set_update_period(1, INACTIVITY_PERIOD);
						return 0;
					}

										
					//	если dispatch_left_side_menu отработал, то завершаем наше приложение, т.к. данные экрана уже выгрузились
					// на этом этапе уже выполняется новый экран (тот куда свайпнули)
					
					
					Elf_proc_* proc = get_proc_by_addr(main);
					proc->ret_f = NULL;
					
					elf_finish(main);	//	выгрузить Elf из памяти
					return 0;
				} else { 			//	если запуск не из быстрого меню, обрабатываем свайпы по отдельности
					switch (gest->gesture){
						case GESTURE_SWIPE_RIGHT: {	//	свайп направо
							return show_menu_animate(calend->ret_f, (unsigned int)show_calend_screen, ANIMATE_RIGHT);	
							break;
						}
						case GESTURE_SWIPE_LEFT: {	// справа налево
							//	действие при запуске из меню и дальнейший свайп влево
							
							
							break;
						}
					} /// switch (gest->gesture)
				}

			break;
		};	//	case GESTURE_SWIPE_LEFT:
		
		
		case GESTURE_SWIPE_UP: {	// свайп вверх
			if ( calend->month < 12 ) 
					calend->month++;
			else {
					calend->month = 1;
					calend->year++;
			}
			
			if ( (calend->year == datetime.year) && (calend->month == datetime.month) )
				day = datetime.day;
			else	
				day = 0;
			draw_month(day, calend->month, calend->year);
			repaint_screen_lines(1, 176);
			
			// продлить таймер выхода при бездействии через INACTIVITY_PERIOD с
			set_update_period(1, INACTIVITY_PERIOD);
			break;
		};
		case GESTURE_SWIPE_DOWN: {	// свайп вниз
			if ( calend->month > 1 ) 
					calend->month--;
			else {
					calend->month = 12;
					calend->year--;
			}
			
			if ( (calend->year == datetime.year) && (calend->month == datetime.month) )
				day = datetime.day;
			else	
				day = 0;
			draw_month(day, calend->month, calend->year);			
			repaint_screen_lines(1, 176);
			
			// продлить таймер выхода при бездействии через INACTIVITY_PERIOD с
			set_update_period(1, INACTIVITY_PERIOD);
			break;
		};		
		default:{	// что-то пошло не так...
			break;
		};		
		
	}
	
	
	return result;
};
