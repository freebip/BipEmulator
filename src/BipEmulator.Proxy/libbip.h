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

// �����
#define COLOR_BLACK     0x000000        //  ������
#define COLOR_RED       0x0000FF        //  �������
#define COLOR_GREEN     0x00FF00        //  �������
#define COLOR_BLUE      0xFF0000        //  �����
#define COLOR_YELLOW    0x00FFFF        //  ������
#define COLOR_AQUA      0xFFFF00        //  ���� ������� �����
#define COLOR_PURPLE    0xFF00FF        //  ���������
#define COLOR_WHITE     0xFFFFFF        //  �����
#define COLOR_MASK      0xFFFFFF        //  ����� �����

// ����� � �������� �������
#define COLOR_SH_BLACK      0b0000      //  ������
#define COLOR_SH_RED        0b0001      //  �������
#define COLOR_SH_GREEN      0b0010      //  �������
#define COLOR_SH_BLUE       0b0100      //  �����
#define COLOR_SH_YELLOW     0b0011      //  ������
#define COLOR_SH_AQUA       0b0110      //  ���� ������� �����
#define COLOR_SH_PURPLE     0b0101      //  ���������
#define COLOR_SH_WHITE      0b0111      //  �����
#define COLOR_SH_MASK       0b1111      //  ����� �����

// ����� ������ (������)
#define locale_ru_RU    1049    //  �������
#define locale_en_US    1033    //  ����������
#define locale_it_IT    1040    //  �����������
#define locale_es_ES    3082    //  ���������
#define locale_fr_FR    1036    //  �����������
#define locale_de_DE    1031    //  ��������
#define locale_el_GR    1032    //  ���������


//  ��������� ����� ��� ��������� ������� �� ����� � ������� ������
struct gesture_ {
    char    gesture;        // ��� �����
    int     touch_pos_x,    // ���������� �������   X
        touch_pos_y;    //                      Y
};

// ��� �����
#define GESTURE_CLICK           1 // ������� ������� �� �����, ���������� ������� � ���������� touch_pos_x touch_pos_y
#define GESTURE_SWIPE_UP        2 // ����� ����� �����
#define GESTURE_SWIPE_DOWN      3 // ����� ������ ����
#define GESTURE_SWIPE_LEFT      4 // ����� ������ ������
#define GESTURE_SWIPE_RIGHT     5 // ����� ����� �������

// ��� �������� ��� ������� show_menu_animate
#define ANIMATE_LEFT        0   // �������� �������� ������ ������ ������
#define ANIMATE_RIGHT       1   // �������� �������� ������ ����� �������
#define ANIMATE_UP          2   // �������� �������� ������ ����� �����
#define ANIMATE_DOWN        3   // �������� �������� ������ ������ ����

// ������ ��������
#define LATIN_1_1_5_12      11512
#define LATIN_1_1_5_16      11516
#define LATIN_1_1_5_36      11536
#define LATIN_1_1_5_56      11556
#define NOT_LATIN_1_1_2_05  11205
#define UNI_LATIN           55555


typedef struct {                //  ��������� ����������� ��������
    unsigned int    process_id; //  ������������� ��������, ������������� �����������
    int         index_listed;   //  ������ ����� � ������ ����������
    void* base;       //  ��������� �� ���������� ��� ������� ������
    unsigned int    size;       //  ���������� ���������� ��� ������� ������
    void* ret_f;      //  ����� �������� ��������
    unsigned int    ret_param0; //  �������� ������� ��������
    void(*elf_finish)(void* param0);    //  ��������� �� ��������� ���������� �����, ���� ���� �������� 
    void(*entry_point)(void* param0);   //  ��������� �� ��������� ������� �����, ����� �����. Param0 = ��������� �� ��������� proc ����������� �������� 
    void* param;      //  ���������������� ��������, ����� ������� ��� ������, �������� ��������� ������ temp_buf_2 ��� ������� ���������             
    int             argc;       //  ���������� ���������� ��� ������� �����
    void** argv;       //  ��������� ��� ������� �����
} Elf_proc_;


// ��������� ������
struct regmenu_ {
    unsigned    char     curr_scr;                  //  ����� ������
    unsigned    char     swipe_scr;                 //  �������������� ����� ������
    unsigned    char     overlay;                   //  ������� ����������� ������
    void* dispatch_func,             //  ������� ��������� ������ ������
        * key_press,                 //  ������� ��������� ������� �� ������� ������
        * scr_job_func,              //  ������ ������� ������� ���������� ������
        * scr_job_buff,              //  ���������� ��� ������� ���������� ������ 
        * show_menu_func,            //  ������� ������������ ������
        * show_menu_buff,            //  ���������� ��� ������� ������������ ������ 
        * long_key_press;            //  ������� ��������� ������� ������� �� ������� ������
};

// ��������� ���� �������
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

// ��������� ����������� ��������
struct res_params_ {
    short width;      //  ������ � ��
    short height;     //  ������ � ��     
};

// ��������� ������ ������� ������
// 1.1.5.12, 1.1.5.36
typedef struct {
    int             t_start;
    short           last_hr;
    unsigned char   field_2;
    unsigned char   field_3;
    unsigned char   field_4;
    unsigned char   heart_rate;         //  �������, ������/���; >200 - �������� �� ��������
    unsigned char   ret_code;           //  ������ ��������� 0-���������, >0 ��������� �� ���������
    unsigned char   field_5;
} hrm_data_struct;

// 1.1.2.05
typedef struct {
    int             t_start;
    short           last_hr;
    unsigned char   heart_rate;         //  �������, ������/���; >200 - �������� �� ��������
    unsigned char   ret_code;           //  ������ ��������� 0-���������, >0 ��������� �� ���������
}  hrm_data_struct_legacy;


//  ������������ ������ (��� ������� get_navi_data)
typedef struct {
    int ready; // ���������� ������: bit 0: �������� ; bit 1: ������  ; bit 2: ������  ; bit 3: �������
    unsigned int pressure; // �������� �������� � ��������
    float altitude; // �������� ������ � ������
    signed int latitude; // ������ �������� ������� � ��������, ���������� �� 3000000
    int ns; // ns: 0-�������� ���������; 1-�����
    signed int longitude; // ������ ������� ������� � ��������, ���������� �� 3000000
    int ew; // ew: 2-�������� ���������; 3-���������; 
} navi_struct_;

// ���������
#define NAVI_NORTH_HEMISPHERE   0   //  �������� ���������
#define NAVI_SOUTH_HEMISPHERE   1   //  ����� ���������
#define NAVI_WEST_HEMISPHERE    2   //  �������� ���������
#define NAVI_EAST_HEMISPHERE    3   //  �������� ���������

// ������� ��� �������� �������� ����������
#define IS_NAVI_PRESS_READY(navi)       (navi & 0x01)   //  ���������� ������ ������������ ��������: 0 - �� �����, 1 - �����
#define IS_NAVI_GPS_READY(navi)         (navi & 0x0E)   //  ���������� ���������: 0 - �� �����, 1 - �����   
#define IS_NAVI_GPS_ALT_READY(navi)     (navi & 0x02)   //  ���������� ������ ������ (GPS): 0 - �� �����, 1 - �����
#define IS_NAVI_GPS_LAT_READY(navi)     (navi & 0x04)   //  ���������� ������ ������ (GPS): 0 - �� �����, 1 - �����
#define IS_NAVI_GPS_LONG_READY(navi)    (navi & 0x08)   //  ���������� ������ ������� (GPS): 0 - �� �����, 1 - �����

// ��� ������� ������ ������, ������ � ������� ������ ��� �������� GPS ���������, ��� ��� ���������� ��������� 
//  ������ ���������� GPS  - IS_NAVI_GPS_READY(navi) 

//  ���� �����������
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


// ��������� �� ������ ������
void* get_ptr_screen_memory();

unsigned char get_var_menu_overlay();

int vibrate(int count, int on_ms, int off_ms);
// ��������� ������
void load_font();
// ����������� ������������ ������ � ������������ �������
void reg_menu(void* regmenu_, int param);
void* pvPortMalloc(int size);
int _memclr(void* buf, int len);
//  ���������� � �������� ������� memcpy
int _memcpy(void* dest, const void* srcptr, int num);
//  ���������� � �������� ������� memset
int _memset(void* buf, int len, int val);
//    ���������� � �������� ������� memcmp
int _memcmp(const void* p1, const void* p2, int size);                       

int show_watchface();
void* get_ptr_temp_buf_2();

// ������ ������� �������� ������. ������ ������� screen_job_func; cmd=0 ���������� ������, cmd=1 ����� ������� �� ���������� �� ������ period 
int set_update_period(int cmd, int period);

//    ����� �������� ����� ��� ����������� ����������� ��������
void set_bg_color(long color);
// ������������� ������ ������� ���������� ����������� � ������������ � �������� (�������� ��������)
int set_graph_callback_to_ram_1();
// ����� ����� ��� ����������� ����������� ��������
void set_fg_color(long color);
// ������� ������ ������ ����
void fill_screen_bg();
//  ����� ������ �� �����������, pos_x, pos_y ���������� �������� �������� ���� �������
void text_out_center(const char* text, int pos_center_x, int pos_y);
//  ����� ������ �� �����������, pos_x, pos_y ���������� �������� ������ ���� �������
void text_out(const char* text, int pos_x, int pos_y);

// �������� �� ������ ������ (����������� �� ����������� � �������)
void repaint_screen_lines(int from, int to);
// ����������� ���� � ��������� ������ ������, param - �������� ������������ ������� show_menu_function
int show_menu_animate(void* show_menu_function, int param, int animate);
// ��������� �������������� �����
void draw_horizontal_line(int pos_y, int from_x, int to_x);
// ��������� ������������ �����
void draw_vertical_line(int pos_x, int from_y, int to_y);

// ���������� ���������� ����� ���������� ������� � ������� ������������ (�������� 510 ����� � �������)
int get_tick_count();
// ���������� � �������� ������� sprintf
int _sprintf(char* buf, const char* format, ...);
//  ��������� ���������� ������
int set_display_state_value(int state_1, int state);
// ��������� ������������ ������ ���� �������������� 
void draw_filled_rect_bg(int from_x, int from_y, int to_x, int to_y);
//  ������ ������ �������� ����������� �����
int ElfReadSettings(int index_listed, void* buffer, int offset, int len);
// ������ ������ �������� ����������� ����� 
int ElfWriteSettings(int index_listed, void* buffer, int offset, int len);
// ����������� ������������ ������� �� ��� ������
void show_res_by_id(int res_ID, int pos_x, int pos_y);
// ���������� �� ������ ����������� ������ ����������� �����, ������������ � ������ .elf.resources
// ��������� �������� ���������� � 0 ��� ������� �����  
int show_elf_res_by_id(int index_listed, int res_id, int pos_x, int pos_y);
//  ������������� ���������� ��������� �����
extern void _srand(unsigned int seed);
// ������ � ��� ����������, debug_level=5
int log_printf(int debug_level, const char* format, ...);
// ��������� ��������������
void draw_rect(int from_x, int from_y, int to_x, int to_y);
//  ���������� ������ � ������� ����/�������
int get_current_date_time(struct datetime_* datetime);
//  ��������� ������� ������� �������� ����
int dispatch_left_side_menu(struct gesture_* gest);
// ��������� ������ �� ������ ���������� ����� ���� ����
int get_selected_locale();
//    ���������� ������ ������, ������� ����� ������� ������� �������
int text_width(const char* text);
// ���������� ������ ������, �������� ������
int get_text_height();
// ���������� ���������� left_side_menu_active
int get_left_side_menu_active();
// ��������� �� ������� �������� �������� ������
void* get_ptr_show_menu_func();
//  ���������� ��������� �� ��������� ����������� �������� �������� �� ������ �������
Elf_proc_* get_proc_by_addr(void* addr);
// ������� ���������� ����������, � �������� ��������� ���������� �������� ����� �������� ������� ����������� ������ ����������
void elf_finish(void* addr);
// ��������� ��������������
void draw_rect(int from_x, int from_y, int to_x, int to_y);
// ��������� ������������ ��������������
void draw_filled_rect(int from_x, int from_y, int to_x, int to_y);
// ���������� � �������� ������� strlen
int _strlen(const char* str);
//  ���������� ������ �������� 
int get_fw_version();
// ����������� ���� ������� �������
void show_big_digit(int symbolSet, const char* digits, int pos_x, unsigned int pos_y, int space);
// ���������� ��������� res_params_ � ��������� ������������ �������
int get_res_params(int index_listed, int res_id, struct res_params_* res_params);
// ������������� ������� ������� �� ����� ��
void vTaskDelay(int delay_ms);




// ��������� ����������� ���������� �������� � ������� ������������
unsigned char get_last_heartrate();
// �������� ������ ��������� ������ 0x20 - �����������
int set_hrm_mode(int hrm_mode);
// ��������� ��������� �� ������ ������� ������
void* get_hrm_struct();                                                       

// ������� ���������
// ���������/���������� �������� GPS � ���������
void switch_gps_pressure_sensors(int mode);
// ��������� ������ GPS � ���������
void get_navi_data(navi_struct_* navi_data);
// �������� ���������� GPS
int is_gps_fixed();

// internal

struct global_app_data_t
{
    // ����� ������� ��������
    void* ret_f;
};

int main(int param0, char** argv);
int main_proxy();

void regmenu_show_screen(void* param);
void regmenu_screen_job();
void regmenu_long_keypress();
void regmenu_keypress();
int regmenu_dispatch_func(void* param);

