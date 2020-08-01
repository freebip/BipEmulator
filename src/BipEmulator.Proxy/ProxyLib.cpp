#include <Windows.h>
#include <stdio.h>
#include <ctime>
#include <iostream>
#include <fstream>

#include "libbip.h"

using namespace System;

namespace BipEmulatorProxy
{
    public ref class ProxyLib
    {
    public:

        delegate Object^ Callback(array<Object^>^);
        property static Callback^ CallbackFunc
        {
        public:Callback^ get() { return _callbackFunc; }
        private:void set(Callback^ value) { _callbackFunc = value; }
        }
    private:
        static Callback^ _callbackFunc;
        static int _updatePeriod = 0;
        static System::Threading::Thread^ _timerThread;

    public:

        // DIRECT RUN

        static int MainRun()
        {
            return main_proxy();
        }

        static void TimerThreadFunc()
        {
            while (true)
            {
                if (_updatePeriod == 0)
                {
                    _timerThread->Sleep(50);
                    continue;
                }

                _timerThread->Sleep(_updatePeriod);
                _updatePeriod = 0;
                regmenu_screen_job();
            }
        }

        static void SetUpdateParams(int cmd, int period)
        {
            if (cmd == 0)
            {
                _updatePeriod = 0;
            }
            else
            {
                _updatePeriod = period;
            }
        }

        static int RegMenuDispatchScreen(unsigned char gesture, int x, int y)
        {
            struct gesture_ g;
            g.gesture = gesture;
            g.touch_pos_x = x;
            g.touch_pos_y = y;
            return regmenu_dispatch_func(&g);
        }

        static void RegMenuKeyPress()
        {
            regmenu_keypress();
        }

        static void RegMenuLongKeyPress()
        {
            regmenu_long_keypress();
        }

        // CALLBACK

        static void RegisterCallback(Callback^ cb)
        {
            if (_timerThread == nullptr)
            {
                _timerThread = gcnew System::Threading::Thread(gcnew System::Threading::ThreadStart(TimerThreadFunc));
                _timerThread->Start();
            }
            CallbackFunc = cb;
        }

        static void UnregisterCallback()
        {
            _timerThread->Abort();
            _timerThread = nullptr;
            CallbackFunc = nullptr;
        }

        static void CallbackVoid(String^ fName)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(1) { fName };
                CallbackFunc(arr);
            }
        }

        static int CallbackInt(String^ fName)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(1) { fName };
                return safe_cast<int>(CallbackFunc(arr));
            }
            return -1;
        }

        static int CallbackInt(String^ fName, String^ p0)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(2) { fName, p0 };
                return safe_cast<int>(CallbackFunc(arr));
            }
        }

        static void CallbackVoid(String^ fName, int p0)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(2) { fName, p0 };
                CallbackFunc(arr);
            }
        }

        static void CallbackVoid(String^ fName, int p0, int p1)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(3) { fName, p0, p1 };
                CallbackFunc(arr);
            }
        }

        static void CallbackVoid(String^ fName, int p0, int p1, int p2)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(4) { fName, p0, p1, p2 };
                CallbackFunc(arr);
            }
        }

        static void CallbackVoid(String^ fName, int p0, int p1, int p2, int p3)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(5) { fName, p0, p1, p2, p3 };
                CallbackFunc(arr);
            }
        }

        static void CallbackVoid(String^ fName, String^ p0, int p1, int p2)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(4) { fName, p0, p1, p2 };
                CallbackFunc(arr);
            }
        }

        static void CallbackVoid(String^ fName, int p0, String^ p1, int p2, int p3, int p4)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(6) { fName, p0, p1, p2, p3, p4 };
                CallbackFunc(arr);
            }
        }

        static void CallbackVoid(String^ fName, void* p0)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(2) { fName, gcnew IntPtr(p0) };
                CallbackFunc(arr);
            }
        }

        static int CallbackInt(String^ fName, int p0, int p1)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(3) { fName, p0, p1 };
                return safe_cast<int>(CallbackFunc(arr));
            }
            return -1;
        }

        static int CallbackInt(String^ fName, int p0, int p1, int p2)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(4) { fName, p0, p1, p2 };
                return safe_cast<int>(CallbackFunc(arr));
            }
            return -1;
        }

        static int CallbackInt(String^ fName, int p0, int p1, void *p2)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(4) { fName, p0, p1, gcnew IntPtr(p2) };
                return safe_cast<int>(CallbackFunc(arr));
            }
            return -1;
        }

        static int CallbackInt(String^ fName, int p0, int p1, int p2, int p3)
        {
            if (CallbackFunc != nullptr)
            {
                array<Object^>^ arr = gcnew array<Object^>(5) { fName, p0, p1, p2, p3 };
                return safe_cast<int>(CallbackFunc(arr));
            }
            return -1;
        }

    };
}

const char* SETTINGS_FILE = "settings.bin";

static struct regmenu_* _stored_reg_menu;
static struct global_app_data_t* global_app_data;

HANDLE _shared_memory_handle = nullptr;
void* _shared_memory_buffer = nullptr;


int main_proxy()
{
    // эмулируем первичный запуск

    global_app_data = (struct global_app_data_t*)malloc(sizeof(global_app_data_t));
    global_app_data->ret_f = show_watchface;

    Elf_proc_ proc;
    int res = main((int)&proc, nullptr);
    repaint_screen_lines(0, 176);
    return res;
}

void regmenu_show_screen(void* param)
{
    if (_stored_reg_menu != nullptr && _stored_reg_menu->show_menu_func != nullptr)
        ((void(*)(void*))_stored_reg_menu->show_menu_func)(param);
}

void regmenu_screen_job()
{
    if (_stored_reg_menu != nullptr && _stored_reg_menu->scr_job_func != nullptr)
        ((void(*)())_stored_reg_menu->scr_job_func)();
}

void regmenu_long_keypress()
{
    if (_stored_reg_menu != nullptr && _stored_reg_menu->long_key_press != nullptr)
        ((void(*)())_stored_reg_menu->long_key_press)();
}

void regmenu_keypress()
{
    if (_stored_reg_menu != nullptr && _stored_reg_menu->key_press != nullptr)
        ((void(*)())_stored_reg_menu->key_press)();
}

int regmenu_dispatch_func(void* param)
{
    if (_stored_reg_menu != nullptr && _stored_reg_menu->dispatch_func != nullptr)
        return ((int(*)(void*))_stored_reg_menu->dispatch_func)(param);
    return -1;
}

void reg_menu(void* regmenu_, int param)
{
    _stored_reg_menu = (struct regmenu_*)regmenu_;
    BipEmulatorProxy::ProxyLib::CallbackVoid("reg_menu", param);
}

int vibrate(int count, int on_ms, int off_ms)
{
    return BipEmulatorProxy::ProxyLib::CallbackInt("vibrate", count, on_ms, off_ms);
}

void load_font()
{
    BipEmulatorProxy::ProxyLib::CallbackVoid("load_font");
}

int set_update_period(int cmd, int period)
{
    BipEmulatorProxy::ProxyLib::SetUpdateParams(cmd, period);
    BipEmulatorProxy::ProxyLib::CallbackVoid("set_update_period", cmd, period);
    return 0;
}

void set_bg_color(long color)
{
    BipEmulatorProxy::ProxyLib::CallbackVoid("set_bg_color", color);
}

void set_fg_color(long color)
{
    BipEmulatorProxy::ProxyLib::CallbackVoid("set_fg_color", color);
}

void draw_horizontal_line(int y, int from_x, int to_x)
{
    BipEmulatorProxy::ProxyLib::CallbackVoid("draw_horizontal_line", y, from_x, to_x);
}

void draw_vertical_line(int x, int from_y, int to_y)
{
    BipEmulatorProxy::ProxyLib::CallbackVoid("draw_vertical_line", x, from_y, to_y);
}

void draw_rect(int from_x, int from_y, int to_x, int to_y)
{
    BipEmulatorProxy::ProxyLib::CallbackVoid("draw_rect", from_x, from_y, to_x, to_y);
}

void draw_filled_rect(int from_x, int from_y, int to_x, int to_y)
{
    BipEmulatorProxy::ProxyLib::CallbackVoid("draw_filled_rect", from_x, from_y, to_x, to_y);
}

void draw_filled_rect_bg(int from_x, int from_y, int to_x, int to_y)
{
    BipEmulatorProxy::ProxyLib::CallbackVoid("draw_filled_rect_bg", from_x, from_y, to_x, to_y);
}

void fill_screen_bg()
{
    BipEmulatorProxy::ProxyLib::CallbackVoid("fill_screen_bg");
}

void text_out_center(const char* text, int pos_center_x, int pos_y)
{
    String^ str = System::Text::Encoding::UTF8->GetString((byte*)text, strlen(text));
    BipEmulatorProxy::ProxyLib::CallbackVoid("text_out_center", str, pos_center_x, pos_y);
}

void text_out(const char* text, int pos_x, int pos_y)
{
    String^ str = System::Text::Encoding::UTF8->GetString((byte*)text, strlen(text));
    BipEmulatorProxy::ProxyLib::CallbackVoid("text_out", str, pos_x, pos_y);
}

void repaint_screen_lines(int from, int to)
{
    BipEmulatorProxy::ProxyLib::CallbackVoid("repaint_screen_lines", from, to);
}

void show_res_by_id(int res_id, int pos_x, int pos_y)
{
    BipEmulatorProxy::ProxyLib::CallbackInt("show_res_by_id", res_id, pos_x, pos_y);
}

int show_elf_res_by_id(int index_listed, int res_id, int pos_x, int pos_y)
{
    return BipEmulatorProxy::ProxyLib::CallbackInt("show_elf_res_by_id", index_listed, res_id, pos_x, pos_y);
}

void show_big_digit(int characterSet, const char* text, int x, unsigned int y, int space)
{
    String^ str = System::Text::Encoding::UTF8->GetString((byte*)text, strlen(text));
    BipEmulatorProxy::ProxyLib::CallbackVoid("show_big_digit", characterSet, str, x, y, space);
}

int text_width(const char* text)
{
    String^ str = System::Text::Encoding::UTF8->GetString((byte*)text, strlen(text));
    return BipEmulatorProxy::ProxyLib::CallbackInt("text_width", str);
}

int get_text_height()
{
    return BipEmulatorProxy::ProxyLib::CallbackInt("get_text_height");
}


int get_res_params(int index_listed, int res_id, struct res_params_* res_params)
{
    return BipEmulatorProxy::ProxyLib::CallbackInt("get_res_params", index_listed, res_id, (void*)res_params);
}

unsigned char get_last_heartrate()
{
    return BipEmulatorProxy::ProxyLib::CallbackInt("get_last_heartrate");
}

void* get_hrm_struct()
{
    return (void*)BipEmulatorProxy::ProxyLib::CallbackInt("get_hrm_struct");
}

void get_navi_data(navi_struct_* navi_data)
{
    BipEmulatorProxy::ProxyLib::CallbackVoid("get_navi_data", (void*)navi_data);
}

int is_gps_fixed()
{
    return BipEmulatorProxy::ProxyLib::CallbackInt("is_gps_fixed");
}

unsigned char get_var_menu_overlay()
{
    return 0;
}

void* pvPortMalloc(int size)
{
    return malloc(size);
}

int _memclr(void* buf, int len)
{
    return (int)memset(buf, 0, len);
}

int _memcpy(void* dest, const void* srcptr, int num)
{
    return (int)memcpy(dest, srcptr, num);
}

int _memset(void* buf, int len, int val)
{
    return (int)memset(buf, val, len);
}

int show_watchface()
{
    return 0;
}

void* get_ptr_temp_buf_2()
{
    return global_app_data;
}

int set_graph_callback_to_ram_1()
{
    return 0;
}

int show_menu_animate(void* show_menu_function, int param, int animate)
{
    return 0;
}

int get_tick_count()
{
    return (int)GetTickCount();
}

int _sprintf(char* buf, const char* format, ...)
{
    va_list args;
    va_start(args, format);
    int res = vsprintf(buf, format, args);
    va_end(args);
    return res;
}

int log_printf_proxy(const char* text)
{
    return BipEmulatorProxy::ProxyLib::CallbackInt("log_printf", gcnew String(text));
}

int log_printf(int debug_level, const char* format, ...)
{
    char* log = new char[255];
    va_list args;
    va_start(args, format);
    int res = vsprintf(log, format, args);
    va_end(args);
    return log_printf_proxy(log);
}

int get_current_date_time(struct datetime_* datetime)
{
    std::time_t t = std::time(0);
    std::tm* now = std::localtime(&t);
    datetime->year = now->tm_year + 1900;
    datetime->month = now->tm_mon + 1;
    datetime->day = now->tm_mday;
    datetime->hour = now->tm_hour % 12;
    datetime->min = now->tm_min;
    datetime->sec = now->tm_sec;
    datetime->weekday = now->tm_wday;
    datetime->h24 = now->tm_hour;
    return 0;
}

int set_display_state_value(int state_1, int state)
{
    return 0;
}

int ElfReadSettings(int index_listed, void* buffer, int offset, int len)
{
    std::ifstream infile;
    infile.open(SETTINGS_FILE, std::ios::in | std::ios::binary);
    infile.seekg(offset, infile.beg);
    infile.read((char*)buffer, len);
    infile.close();
    return 0;
}

int ElfWriteSettings(int index_listed, void* buffer, int offset, int len)
{
    std::ofstream outfile;
    outfile.open(SETTINGS_FILE, std::ios::out | std::ios::binary);
    outfile.seekp(offset, outfile.beg);
    outfile.write((char*)buffer, len);
    outfile.close();
    return 0;
}

void _srand(unsigned int seed)
{
    srand(seed);
}

int dispatch_left_side_menu(struct gesture_* gest)
{
    return 0;
}

int get_selected_locale()
{
    return BipEmulatorProxy::ProxyLib::CallbackInt("get_selected_locale");
}

int get_left_side_menu_active()
{
    return 0;
}

void* get_ptr_show_menu_func()
{
    return NULL;
}

Elf_proc_* get_proc_by_addr(void* addr)
{
    return(Elf_proc_*)NULL;
}

void elf_finish(void* addr)
{

}

int _strlen(const char* str)
{
    return strlen(str);
}

int get_fw_version()
{
    return LATIN_1_1_5_36;
}

int set_hrm_mode(int hrm_mode)
{
    return 0;
}

void switch_gps_pressure_sensors(int mode)
{

}

void vTaskDelay(int delay_ms)
{
    System::Threading::Tasks::Task::Delay(delay_ms);
}

void* get_ptr_screen_memory()
{
    if (_shared_memory_handle == nullptr)
    {
        _shared_memory_handle = CreateFileMapping((HANDLE)INVALID_HANDLE_VALUE, nullptr, PAGE_READWRITE, 0, VIDEO_MEMORY_SIZE, L"meme");
        if (_shared_memory_handle != nullptr)
            _shared_memory_buffer = MapViewOfFile(_shared_memory_handle, FILE_MAP_READ | FILE_MAP_WRITE, 0, 0, VIDEO_MEMORY_SIZE);

        if (_shared_memory_buffer != nullptr)
        {
            int res = BipEmulatorProxy::ProxyLib::CallbackInt("__shared_memory_enabled__");
        }
    }

    return _shared_memory_buffer;
}

