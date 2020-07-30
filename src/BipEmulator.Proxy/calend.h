/*
	(С) Волков Максим 2019 ( mr-volkov+bip@yandex.ru )
	Календарь для Amazfit Bip
	
*/
#ifndef __CALEND_H__
#define __CALEND_H__

#include "libbip.h"

// Значения цветовой схемы 
#define CALEND_COLOR_BG					0	//	фон  календаря
#define CALEND_COLOR_MONTH				1	//	цвет названия текущего месяца
#define CALEND_COLOR_YEAR				2	//	цвет текущего года
#define CALEND_COLOR_WORK_NAME			3	//	цвет названий дней будни
#define CALEND_COLOR_HOLY_NAME_BG		4	// 	фон	 названий дней выходные
#define CALEND_COLOR_HOLY_NAME_FG		5	//	цвет названий дней выходные
#define CALEND_COLOR_SEPAR				6	//	цвет разделителей календаря
#define CALEND_COLOR_NOT_CUR_WORK		7	//	цвет чисел НЕ текущего месяца будни
#define CALEND_COLOR_NOT_CUR_HOLY_BG	8	//	фон  чисел НЕ текущего месяца выходные
#define CALEND_COLOR_NOT_CUR_HOLY_FG	9	//	цвет чисел НЕ текущего месяца выходные
#define CALEND_COLOR_CUR_WORK			10	//	цвет чисел текущего месяца будни
#define CALEND_COLOR_CUR_HOLY_BG		11	//	фон  чисел текущего месяца выходные
#define CALEND_COLOR_CUR_HOLY_FG		12	//	цвет чисел текущего месяца выходные
#define CALEND_COLOR_TODAY_BG			13	//	цвет чисел текущего дня
#define CALEND_COLOR_TODAY_FG			14	//	фон  чисел текущего дня

// количество цыетовых схем
#define COLOR_SCHEME_COUNT	5

//	смещение адреса для хранения настроек календаря
#define OPT_OFFSET_CALEND_OPT		0

#if FW_VERSION==latin_1_1_5_12 || FW_VERSION==latin_1_1_5_36
// параметры рисования цифр календаря
//  строка: от 7 до 169 = 162рх в ширину 7 чисел по 24рх на число 7+(24)*6+22+3
//  строки: от 57 до 174 = 117рх в высоту 6 строк по 22рх на строку 1+(22)*5+22

#define CALEND_Y_BASE		30		//	базовая высота начала отрисовки календаря
//#define CALEND_NAME_HEIGHT	19		//	высота строки названий дней недели
//#define CALEND_DAYS_Y_BASE	CALEND_Y_BASE+1+V_MARGIN+CALEND_NAME_HEIGHT+V_MARGIN+1		//	высота базы чисел месяца
#define WIDTH				24		//	ширина цифр числа
#define HEIGHT				19		//	высота цифр числа
#define V_SPACE				0		//	вертикальный отступ между строками чисел
#define	H_SPACE				0		//	горизонтальный отступ между колонками чисел
#define H_MARGIN 			4		//	горизонтальный отступ от края экрана
#define V_MARGIN 			1		//	вертикальный отступ от заголовка (базы)

#elif	FW_VERSION==not_latin_1_1_2_05
// параметры рисования цифр календаря
//  строка: от 7 до 169 = 162рх в ширину 7 чисел по 24рх на число 7+(24)*6+24+3
//  строки: от 57 до 174 = 117рх в высоту 6 строк по 22рх на строку 1+(22)*5+20

#define CALEND_Y_BASE		25		//	базовая высота начала отрисовки календаря
//#define CALEND_NAME_HEIGHT	FONT_HEIGHT+2		//	высота строки названий дней недели
//#define CALEND_DAYS_Y_BASE	CALEND_Y_BASE+1+V_MARGIN+CALEND_NAME_HEIGHT+V_MARGIN+1		//	высота базы чисел месяца 56
#define WIDTH				24		//	ширина цифр числа
#define HEIGHT				20		//	высота цифр числа
#define V_SPACE				0		//	вертикальный отступ между строками чисел
#define	H_SPACE				0		//	горизонтальный отступ между колонками чисел
#define H_MARGIN 			4		//	горизонтальный отступ от края экрана
#define V_MARGIN 			1		//	вертикальный отступ от заголовка (базы)
#endif


#define INACTIVITY_PERIOD		30000		//	время по прошествии которого выходим

// сохраняемые опции календаря
struct calend_opt_ {
		unsigned char	color_scheme;	//	цветовая схема
};

// текущие данные просматриваемого/редактируемого календаря
struct calend_ {
	Elf_proc_* 	proc;				//	указатель на данные запущенного процесса
		void* 	ret_f;				//	адрес функции возврата
unsigned char	color_scheme;		//	цветовая схема
									//	отображаемый месяц
unsigned int 	day;				//	день
unsigned int 	month;				//	месяц
unsigned int 	year;				//	год
};


void show_calend_screen (void *return_screen);
void key_press_calend_screen();
int dispatch_calend_screen (void *param);
void calend_screen_job();

void draw_month(unsigned int day, unsigned int month, unsigned int year);
unsigned char wday(unsigned int day,unsigned int month,unsigned int year);
unsigned char isLeapYear(unsigned int year);
					
#endif