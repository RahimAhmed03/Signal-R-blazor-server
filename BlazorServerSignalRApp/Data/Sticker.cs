namespace BlazorServerSignalRApp.Data;

public class Sticker {
    public Header header { get; set; }
    public Body body { get; set; }

    public class Header {
        public string code { get; set; }
        public string status { get; set; }
    }

    public class Body {
        public List<StickerContent> stickerList { get; set; }
    }

    public class StickerContent {
        public int stickerId { get; set; }
        public string stickerImg { get; set; }
    }
}