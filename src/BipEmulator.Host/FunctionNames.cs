namespace BipEmulator.Host
{
    public static class FunctionNames
    {
        public const string GET_HRM_STRUCT = "get_hrm_struct";
        public const string GET_LAST_HEARTRATE = "get_last_heartrate";
        public const string GET_NAVI_DATA = "get_navi_data";
        public const string GET_RES_PARAMS = "get_res_params";
        public const string GET_SELECTED_LOCALE = "get_selected_locale";
        public const string GET_TEXT_HEIGHT = "get_text_height";
        public const string DRAW_FILLED_RECT = "draw_filled_rect";
        public const string DRAW_FILLED_RECT_BG = "draw_filled_rect_bg";
        public const string DRAW_HORIZONTAL_LINE = "draw_horizontal_line";
        public const string DRAW_RECT = "draw_rect";
        public const string DRAW_VERTICAL_LINE = "draw_vertical_line";
        public const string IS_GPS_FIXED = "is_gps_fixed";
        public const string FILL_SCREEN_BG = "fill_screen_bg";
        public const string LOAD_FONT = "load_font";
        public const string LOG_PRINTF = "log_printf";
        public const string REG_MENU = "reg_menu";
        public const string REPAINT_SCREEN_LINES = "repaint_screen_lines";
        public const string SET_BG_COLOR = "set_bg_color";
        public const string SET_FG_COLOR = "set_fg_color";
        public const string SET_UPDATE_PERIOD = "set_update_period";
        public const string SHOW_BIG_DIGITS = "show_big_digit";
        public const string SHOW_ELF_RES_BY_ID = "show_elf_res_by_id";
        public const string SHOW_RES_BY_ID = "show_res_by_id";
        public const string TEXT_OUT = "text_out";
        public const string TEXT_OUT_CENTER = "text_out_center";
        public const string TEXT_WIDTH = "text_width";
        public const string VIBRATE = "vibrate";

        // special emulator functions
        public const string SHARED_MEMORY_ENABLED = "__shared_memory_enabled__";
    }
}
