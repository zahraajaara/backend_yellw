namespace YellowKalam.Api.Dtos
{
    // This matches your existing SystemSettings used in React
    public class SystemSettingsDto
    {
        public string DefaultLanguage { get; set; } = "ar";      // ar / en
        public string ThemeMode { get; set; } = "light";         // light / dark
        public string DateFormat { get; set; } = "dd/MM/yyyy";
        public int PageSizeDefault { get; set; } = 20;
    }

    // Barcode layout (frmBarCode)
    public class BarcodeSettingsDto
    {
        public int Columns { get; set; } = 1;
        public int Rows { get; set; } = 1;
        public int ColumnPadding { get; set; } = 1;
        public int RowPadding { get; set; } = 1;

        public int BarcodeX { get; set; } = 0;
        public int BarcodeY { get; set; } = 25;

        public int ItemNameX { get; set; } = 0;
        public int ItemNameY { get; set; } = 25;

        public int ItemDescX { get; set; } = 0;
        public int ItemDescY { get; set; } = 25;

        public int FontSize { get; set; } = 6;
    }

    // Application settings (frmSettings – values from Settings.Default)
    public class ApplicationSettingsDto
    {
        // Paths
        public string Qba { get; set; } = string.Empty;     // backup path
        public string Dpa { get; set; } = string.Empty;     // documents path
        public string NtPath { get; set; } = string.Empty;  // NTPath
        public string ScannerSc { get; set; } = string.Empty;

        // SMS
        public string HttpR { get; set; } = string.Empty;
        public string HttpUser { get; set; } = string.Empty;
        public string HttpPassword { get; set; } = string.Empty;
        public string HttpSender { get; set; } = string.Empty;
        public string HttpText { get; set; } = string.Empty;
        public string NoAccept { get; set; } = string.Empty;
        public string Review { get; set; } = string.Empty;

        // Permissions/flags
        public bool UserCanAdd { get; set; } = true;
        public bool UserCanEdit { get; set; } = true;
        public bool UserCanDelete { get; set; } = true;
        public bool AllowIhaler { get; set; } = true;
        public bool ChPer { get; set; } = true;
        public bool RegNPer { get; set; } = false;

        public string DefaultInboxUser { get; set; } = string.Empty;
        public int? AppGIdIhaler { get; set; }

        // Misc
        public string SepChar { get; set; } = "|";
        public string StartColor { get; set; } = "#FFFFFF";
        public string EndColor { get; set; } = "#FFFFFF";
    }
}
