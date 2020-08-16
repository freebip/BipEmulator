#pragma once

#define LIBBIP_VERSION "1.3"

#define VIDEO_X     176
#define VIDEO_Y     176

#ifndef min
#define min(x,y) ((x) < (y) ? (x) : (y))
#endif

#ifndef max
#define max(x,y) ((x) > (y) ? (x) : (y))
#endif

#ifndef abssub
#define abssub(x,y) ((x) > (y) ? (x)-(y) : (y)-(x))
#endif

#define VIDEO_MEMORY_SIZE 15488

#define NULL ((void*)0)

// цвета
#define COLOR_BLACK     0x000000        //  черный
#define COLOR_RED       0x0000FF        //  красный
#define COLOR_GREEN     0x00FF00        //  зеленый
#define COLOR_BLUE      0xFF0000        //  синий
#define COLOR_YELLOW    0x00FFFF        //  желтый
#define COLOR_AQUA      0xFFFF00        //  цвет морской волны
#define COLOR_PURPLE    0xFF00FF        //  сиреневый
#define COLOR_WHITE     0xFFFFFF        //  белый
#define COLOR_MASK      0xFFFFFF        //  маска цвета

// цвета в коротком формате
#define COLOR_SH_BLACK      0b0000      //  черный
#define COLOR_SH_RED        0b0001      //  красный
#define COLOR_SH_GREEN      0b0010      //  зеленый
#define COLOR_SH_BLUE       0b0100      //  синий
#define COLOR_SH_YELLOW     0b0011      //  желтый
#define COLOR_SH_AQUA       0b0110      //  цвет морской волны
#define COLOR_SH_PURPLE     0b0101      //  сиреневый
#define COLOR_SH_WHITE      0b0111      //  белый
#define COLOR_SH_MASK       0b1111      //  маска цвета

// языки текста (локали)
#define locale_ru_RU    1049    //  русский
#define locale_en_US    1033    //  английский
#define locale_it_IT    1040    //  итальянский
#define locale_es_ES    3082    //  испанский
#define locale_fr_FR    1036    //  французский
#define locale_de_DE    1031    //  немецкий
#define locale_el_GR    1032    //  греческий


//  структура жеста при обработке нажатий на экран и боковую кнопку
struct gesture_ {
    char    gesture;        // тип жеста
    int     touch_pos_x,    // координаты нажатия   X
        touch_pos_y;    //                      Y
};

// тип жеста
#define GESTURE_CLICK           1 // простое нажатие на экран, координаты нажатия в переменных touch_pos_x touch_pos_y
#define GESTURE_SWIPE_UP        2 // свайп снизу вверх
#define GESTURE_SWIPE_DOWN      3 // свайп сверху вниз
#define GESTURE_SWIPE_LEFT      4 // свайп справа налево
#define GESTURE_SWIPE_RIGHT     5 // свайп слева направо

// вид анимации для функции show_menu_animate
#define ANIMATE_LEFT        0   // анимация перехода экрана справа налево
#define ANIMATE_RIGHT       1   // анимация перехода экрана слева направо
#define ANIMATE_UP          2   // анимация перехода экрана снизу вверх
#define ANIMATE_DOWN        3   // анимация перехода экрана сверху вниз

// версии прошивок
#define LATIN_1_1_5_12      11512
#define LATIN_1_1_5_16      11516
#define LATIN_1_1_5_36      11536
#define LATIN_1_1_5_56      11556
#define NOT_LATIN_1_1_2_05  11205
#define UNI_LATIN           55555


typedef struct {                //  структура запущенного процесса
    unsigned int    process_id; //  идентификатор процесса, присваивается загрузчиком
    int         index_listed;   //  индекс эльфа в списке загрузчика
    void* base;       //  указатель на выделенную под процесс память
    unsigned int    size;       //  количество выделенной под процесс памяти
    void* ret_f;      //  точка возврата процесса
    unsigned int    ret_param0; //  параметр функции возврата
    void(*elf_finish)(void* param0);    //  указатель на процедуру завершения эльфа, сюда надо передать 
    void(*entry_point)(void* param0);   //  указатель на процедуру запуска эльфа, точка входа. Param0 = указатель на структуру proc запущенного процесса 
    void* param;      //  пользовательский параметр, можно хранить что угодно, например указатели вместо temp_buf_2 для фоновых процессов             
    int             argc;       //  количество параметров при запуске эльфа
    void** argv;       //  параметры при запуске эльфа
} Elf_proc_;


// структура экрана
struct regmenu_ {
    unsigned    char     curr_scr;                  //  номер экрана
    unsigned    char     swipe_scr;                 //  дополниетльный номер экрана
    unsigned    char     overlay;                   //  признак оверлейного экрана
    void* dispatch_func,             //  функция обработки жестов экрана
        * key_press,                 //  функция обработки нажатия на боковую кнопку
        * scr_job_func,              //  колбэк функция таймера обновления экрана
        * scr_job_buff,              //  переменная для колбэка обновления экрана 
        * show_menu_func,            //  функция формирования экрана
        * show_menu_buff,            //  переменная для функции формирования экрана 
        * long_key_press;            //  функция обработки долгого нажатия на боковую кнопку
};

// структура даты времени
struct datetime_ {
    unsigned short year;
    unsigned char   month,
        day,
        hour,
        min,
        sec,
        weekday;
    unsigned char   h24;
};

// параметры графических ресурсов
struct res_params_ {
    short width;      //  ширина в рх
    short height;     //  высота в рх     
};

// структуры данных датчика сердца
// 1.1.5.12, 1.1.5.36
typedef struct {
    int             t_start;
    short           last_hr;
    unsigned char   field_2;
    unsigned char   field_3;
    unsigned char   field_4;
    unsigned char   heart_rate;         //  частота, ударов/мин; >200 - значение не доступно
    unsigned char   ret_code;           //  статус измерения 0-закончено, >0 измерение не закончено
    unsigned char   field_5;
} hrm_data_struct;

// 1.1.2.05
typedef struct {
    int             t_start;
    short           last_hr;
    unsigned char   heart_rate;         //  частота, ударов/мин; >200 - значение не доступно
    unsigned char   ret_code;           //  статус измерения 0-закончено, >0 измерение не закончено
}  hrm_data_struct_legacy;


//  навгационные данные (для функции get_navi_data)
typedef struct {
    int ready; // Готовность данных: bit 0: давление ; bit 1: высота  ; bit 2: широта  ; bit 3: долгота
    unsigned int pressure; // значение давления в Паскалях
    float altitude; // значение высоты в метрах
    signed int latitude; // модуль значения долготы в градусах, умноженное на 3000000
    int ns; // ns: 0-северное полушарие; 1-южное
    signed int longitude; // модуль знаения долготы в градусах, умноженное на 3000000
    int ew; // ew: 2-западное полушарие; 3-восточное; 
} navi_struct_;

// полушария
#define NAVI_NORTH_HEMISPHERE   0   //  северное полушарие
#define NAVI_SOUTH_HEMISPHERE   1   //  южное полушарие
#define NAVI_WEST_HEMISPHERE    2   //  западное полушарие
#define NAVI_EAST_HEMISPHERE    3   //  воточное полушарие

// макросы для проверки значения готовности
#define IS_NAVI_PRESS_READY(navi)       (navi & 0x01)   //  готовность данных атмосферного давления: 0 - не готов, 1 - готов
#define IS_NAVI_GPS_READY(navi)         (navi & 0x0E)   //  готовность координат: 0 - не готов, 1 - готов   
#define IS_NAVI_GPS_ALT_READY(navi)     (navi & 0x02)   //  готовность данных высоты (GPS): 0 - не готов, 1 - готов
#define IS_NAVI_GPS_LAT_READY(navi)     (navi & 0x04)   //  готовность данных широты (GPS): 0 - не готов, 1 - готов
#define IS_NAVI_GPS_LONG_READY(navi)    (navi & 0x08)   //  готовность данных долготы (GPS): 0 - не готов, 1 - готов

// как правило данные высоты, широты и долготы готовы при фиксации GPS приемника, так что достаточно проверять 
//  статус готовности GPS  - IS_NAVI_GPS_READY(navi) 

//  Типы уведомлений
#define NOTIFY_TYPE_NONE        0
#define NOTIFY_TYPE_SMS         5
#define NOTIFY_TYPE_EMAIL       6
#define NOTIFY_TYPE_MICHAT      7
#define NOTIFY_TYPE_FACEBOOK    8
#define NOTIFY_TYPE_TWITTER     9
#define NOTIFY_TYPE_MI          10
#define NOTIFY_TYPE_WHATSAPP    12
#define NOTIFY_TYPE_ALARM       16
#define NOTIFY_TYPE_INSTAGRAM   18
#define NOTIFY_TYPE_ALIPAY      22
#define NOTIFY_TYPE_CALENDAR    27
#define NOTIFY_TYPE_VIBER       29
#define NOTIFY_TYPE_TELEGRAM    31
#define NOTIFY_TYPE_SKYPE       33
#define NOTIFY_TYPE_VK          34
#define NOTIFY_TYPE_CALL        39
#define NOTIFY_TYPE_LOW_BAT     42


// указатель на память экрана
void* get_ptr_screen_memory();

unsigned char get_var_menu_overlay();

int vibrate(int count, int on_ms, int off_ms);
// подгрузка шрифта
void load_font();
// регистрация создаваемого экрана в операционной системе
void reg_menu(void* regmenu_, int param);
void* pvPortMalloc(int size);
int _memclr(void* buf, int len);
//  встроенная в прошивку функция memcpy
int _memcpy(void* dest, const void* srcptr, int num);
//  встроенная в прошивку функция memset
int _memset(void* buf, int len, int val);
//    встроенная в прошивку функция memcmp
int _memcmp(const void* p1, const void* p2, int size);                       

int show_watchface();
void* get_ptr_temp_buf_2();

// запуск таймера текущего экрана. колбэк таймера screen_job_func; cmd=0 остановить таймер, cmd=1 взвод таймера на количество мс равное period 
int set_update_period(int cmd, int period);

//    выбор фонового цвета для последующих графических опреаций
void set_bg_color(long color);
// использование данной функции необходимо производить в соответствии с примером (классное описание)
int set_graph_callback_to_ram_1();
// выбор цвета для последующих графических опреаций
void set_fg_color(long color);
// заливка экрана цветом фона
void fill_screen_bg();
//  вывод текста по координатам, pos_x, pos_y координаты середины верхнего края надписи
void text_out_center(const char* text, int pos_center_x, int pos_y);
//  вывод текста по координатам, pos_x, pos_y координаты верхнего левого угла надписи
void text_out(const char* text, int pos_x, int pos_y);

// обновить на экране строки (копирование из видеопамяти в дисплей)
void repaint_screen_lines(int from, int to);
// отображение меню с анимацией сдвига экрана, param - параметр передаваемый функции show_menu_function
int show_menu_animate(void* show_menu_function, int param, int animate);
// отрисовка горизонтальной линии
void draw_horizontal_line(int pos_y, int from_x, int to_x);
// отрисовка вериткальной линии
void draw_vertical_line(int pos_x, int from_y, int to_y);

// возвращает количество тиков системного таймера с момента перезагрузки (примерно 510 тиков в секунду)
int get_tick_count();
// встроенная в прошивку функция sprintf
int _sprintf(char* buf, const char* format, ...);
//  установка параметров экрана
int set_display_state_value(int state_1, int state);
// отрисовка закрашенного цветом фона прямоугольника 
void draw_filled_rect_bg(int from_x, int from_y, int to_x, int to_y);
//  чтение секции настроек конкретного эльфа
int ElfReadSettings(int index_listed, void* buffer, int offset, int len);
// запись секции настроек конкретного эльфа 
int ElfWriteSettings(int index_listed, void* buffer, int offset, int len);
// отображение графического ресурса по его номеру
void show_res_by_id(int res_ID, int pos_x, int pos_y);
// отображает на экране графический ресурс конкретного эльфа, содержащийся в секции .elf.resources
// нумерация ресурсов начинается с 0 для каждого эльфа  
int show_elf_res_by_id(int index_listed, int res_id, int pos_x, int pos_y);
//  инициализация генератора случайных чисел
extern void _srand(unsigned int seed);
// запись в лог устройства, debug_level=5
int log_printf(int debug_level, const char* format, ...);
// отрисовка прямоугольника
void draw_rect(int from_x, int from_y, int to_x, int to_y);
//  возвращает данные о текущей дате/времени
int get_current_date_time(struct datetime_* datetime);
//  процедура разбора свайпов быстрого меню
int dispatch_left_side_menu(struct gesture_* gest);
// получение локали на основе выбранного языка меню мода
int get_selected_locale();
//    возвращает ширину текста, который будет выведен текущим шрифтом
int text_width(const char* text);
// возвращает высоту текста, текущего шрифта
int get_text_height();
// возвращает переменную left_side_menu_active
int get_left_side_menu_active();
// указатель на функцию создания текущего экрана
void* get_ptr_show_menu_func();
//  возвращает указатель на структуру запущенного процесса вычисляя по адресу символа
Elf_proc_* get_proc_by_addr(void* addr);
// функция завершения приложения, в качестве параметра необходимо передать адрес основной функции отображения экрана приложения
void elf_finish(void* addr);
// отрисовка прямоугольника
void draw_rect(int from_x, int from_y, int to_x, int to_y);
// отрисовка закрашенного прямоугольника
void draw_filled_rect(int from_x, int from_y, int to_x, int to_y);
// встроенная в прошивку функция strlen
int _strlen(const char* str);
//  возвращает версию прошивки 
int get_fw_version();
// отображение цифр большим шрифтом
void show_big_digit(int symbolSet, const char* digits, int pos_x, unsigned int pos_y, int space);
// возвращает структуру res_params_ с размерами графического ресурса
int get_res_params(int index_listed, int res_id, struct res_params_* res_params);
// приостановить текущий процесс на время мс
void vTaskDelay(int delay_ms);




// получение измеренного последнего значения с датчика сердцебиения
unsigned char get_last_heartrate();
// становка режима измерения пульса 0x20 - однократный
int set_hrm_mode(int hrm_mode);
// получение указателя на данные датчика сердца
void* get_hrm_struct();                                                       

// Функции навигации
// включение/отключение сенсоров GPS и барометра
void switch_gps_pressure_sensors(int mode);
// получение данных GPS и барометра
void get_navi_data(navi_struct_* navi_data);
// проверка готовности GPS
int is_gps_fixed();

// internal

struct global_app_data_t
{
    // адрес функции возврата
    void* ret_f;
};

int main(int param0, char** argv);
int main_proxy();

void regmenu_show_screen(void* param);
void regmenu_screen_job();
void regmenu_long_keypress();
void regmenu_keypress();
int regmenu_dispatch_func(void* param);

