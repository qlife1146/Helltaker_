using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Helltaker {
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window {
        Bitmap original;
        Bitmap[] frames = new Bitmap[12];
        ImageSource[] imgFrame = new ImageSource[12];
        string azazelPath = "Resources/Azazel.png";
        string cerberusPath = "Resources/Cerberus.png";
        //string helltakerPath = "Resources/Helltaker.png";
        string judgementPath = "Resources/Judgement.png";
        string justicePath = "Resources/Justice.png";
        string luciferPath = "Resources/Lucifer.png";
        string malinaPath = "Resources/Malina.png";
        string modeusPath = "Resources/Modeus.png";
        string pandemonicaPath = "Resources/Pandemonica.png";
        //string skeletonPath = "Resources/Skeleton.png";
        string zdradaPath = "Resources/Zdrada.png";

        int frame = -1;
        double delay = 2.0; //animation speed setting
        double hz = 60;     //don't touch this, it just test.

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public MainWindow() {
            InitializeComponent();
            original = System.Drawing.Image.FromFile(luciferPath) as Bitmap;
            for (int i = 0; i < 12; i++) {
                frames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(frames[i])) {

                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100),
                        new System.Drawing.Rectangle(i * 100, 0, 100, 100),
                        GraphicsUnit.Pixel);
                }
                var handle = frames[i].GetHbitmap();
                try {
                    imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                } finally {
                    DeleteObject(handle);
                }
            }
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds((1/hz) * delay);
            timer.Tick += NextFrame;
            timer.Start();
            this.Topmost = true;

            MouseDown += MainWindow_MouseDown;

            var menu = new System.Windows.Forms.ContextMenu();
            var noti = new System.Windows.Forms.NotifyIcon{
                Icon = System.Drawing.Icon.FromHandle(frames[0].GetHicon()),
                Visible = true,
                Text = "Helltaker",
                ContextMenu = menu,
            };
            var exit = new System.Windows.Forms.MenuItem {
                Index = 1,
                Text = "종료",
            };
            exit.Click += (object o, EventArgs e) => {
                Application.Current.Shutdown();
            };
            var onTop = new System.Windows.Forms.MenuItem {
                Index = 2,
                Text = "항상 위 켜짐",
            };
            onTop.Click += (object o, EventArgs e) => {
                if (this.Topmost == true) {
                    this.Topmost = false;
                    onTop.Text = "항상 위 꺼짐";
                } else {
                    this.Topmost = true;
                    onTop.Text = "항상 위 켜짐";
                }
            };
            //캐릭터 변수
            var Azazel = new System.Windows.Forms.MenuItem {
                Text = "아자젤",
            };
            var Cerberus = new System.Windows.Forms.MenuItem {
                Text = "케르베로스",
            };
            var Helltaker_1 = new System.Windows.Forms.MenuItem {
                Text = "헬테이커 1",
            };
            var Helltaker_2 = new System.Windows.Forms.MenuItem {
                Text = "헬테이커 2",
            };
            var Judgement = new System.Windows.Forms.MenuItem {
                Text = "저지먼트",
            };
            var Justice = new System.Windows.Forms.MenuItem {
                Text = "저스티스",
            };
            var Lucifer_1 = new System.Windows.Forms.MenuItem {
                Text = ">루시퍼 1",
            };
            var Lucifer_2 = new System.Windows.Forms.MenuItem {
                Text = "루시퍼 2",
            };
            var Malina = new System.Windows.Forms.MenuItem {
                Text = "말리나",
            };
            var Modeus = new System.Windows.Forms.MenuItem {
                Text = "모데우스",
            };
            var Pandemonica = new System.Windows.Forms.MenuItem {
                Text = "판데모니카",
            };
            var Skeleton = new System.Windows.Forms.MenuItem {
                Text = "스켈레톤",
            };
            var Zdrada = new System.Windows.Forms.MenuItem {
                Text = "즈드라다",
            };

            //캐릭터 선택
            Azazel.Click += (object o, EventArgs e) => {
                Azazel.Text = ">아자젤";
                Cerberus.Text = "케르베로스";
                Helltaker_1.Text = "헬테이커 1";
                Helltaker_2.Text = "헬테이커 2";
                Judgement.Text = "저지먼트";
                Justice.Text = "저스티스";
                Lucifer_1.Text = "루시퍼 1";
                Lucifer_2.Text = "루시퍼 2";
                Malina.Text = "말리나";
                Modeus.Text = "모데우스";
                Pandemonica.Text = "판데모니카";
                Skeleton.Text = "스켈레톤";
                Zdrada.Text = "즈드라다";

                this.Azazel();
            };
            Cerberus.Click += (object o, EventArgs e) => {
                Azazel.Text = "아자젤";
                Cerberus.Text = ">케르베로스";
                Helltaker_1.Text = "헬테이커 1";
                Helltaker_2.Text = "헬테이커 2";
                Judgement.Text = "저지먼트";
                Justice.Text = "저스티스";
                Lucifer_1.Text = "루시퍼 1";
                Lucifer_2.Text = "루시퍼 2";
                Malina.Text = "말리나";
                Modeus.Text = "모데우스";
                Pandemonica.Text = "판데모니카";
                Skeleton.Text = "스켈레톤";
                Zdrada.Text = "즈드라다";

                this.Cerberus();
            };
            //Helltaker_1.Click += (object o, EventArgs e) => {
            //    Azazel.Text = "아자젤";
            //    Cerberus.Text = "케르베로스";
            //    Helltaker_1.Text = ">헬테이커 1";
            //    Helltaker_2.Text = "헬테이커 2";
            //    Judgement.Text = "저지먼트";
            //    Justice.Text = "저스티스";
            //    Lucifer_1.Text = "루시퍼 1";
            //    Lucifer_2.Text = "루시퍼 2";
            //    Malina.Text = "말리나";
            //    Modeus.Text = "모데우스";
            //    Pandemonica.Text = "판데모니카";
            //    Skeleton.Text = "스켈레톤";
            //    Zdrada.Text = "즈드라다";

            //    this.Helltaker_1();
            //};
            //Helltaker_2.Click += (object o, EventArgs e) => {
            //    Azazel.Text = "아자젤";
            //    Cerberus.Text = "케르베로스";
            //    Helltaker_1.Text = "헬테이커 1";
            //    Helltaker_2.Text = ">헬테이커 2";
            //    Judgement.Text = "저지먼트";
            //    Justice.Text = "저스티스";
            //    Lucifer_1.Text = "루시퍼 1";
            //    Lucifer_2.Text = "루시퍼 2";
            //    Malina.Text = "말리나";
            //    Modeus.Text = "모데우스";
            //    Pandemonica.Text = "판데모니카";
            //    Skeleton.Text = "스켈레톤";
            //    Zdrada.Text = "즈드라다";

            //    this.Helltaker_2();
            //};
            Judgement.Click += (object o, EventArgs e) => {
                Azazel.Text = "아자젤";
                Cerberus.Text = "케르베로스";
                Helltaker_1.Text = "헬테이커 1";
                Helltaker_2.Text = "헬테이커 2";
                Judgement.Text = ">저지먼트";
                Justice.Text = "저스티스";
                Lucifer_1.Text = "루시퍼 1";
                Lucifer_2.Text = "루시퍼 2";
                Malina.Text = "말리나";
                Modeus.Text = "모데우스";
                Pandemonica.Text = "판데모니카";
                Skeleton.Text = "스켈레톤";
                Zdrada.Text = "즈드라다";

                this.Judgement();
            };
            Justice.Click += (object o, EventArgs e) => {
                Azazel.Text = "아자젤";
                Cerberus.Text = "케르베로스";
                Helltaker_1.Text = "헬테이커 1";
                Helltaker_2.Text = "헬테이커 2";
                Judgement.Text = "저지먼트";
                Justice.Text = ">저스티스";
                Lucifer_1.Text = "루시퍼 1";
                Lucifer_2.Text = "루시퍼 2";
                Malina.Text = "말리나";
                Modeus.Text = "모데우스";
                Pandemonica.Text = "판데모니카";
                Skeleton.Text = "스켈레톤";
                Zdrada.Text = "즈드라다";

                this.Justice();
            };
            Lucifer_1.Click += (object o, EventArgs e) => {
                Azazel.Text = "아자젤";
                Cerberus.Text = "케르베로스";
                Helltaker_1.Text = "헬테이커 1";
                Helltaker_2.Text = "헬테이커 2";
                Judgement.Text = "저지먼트";
                Justice.Text = "저스티스";
                Lucifer_1.Text = ">루시퍼 1";
                Lucifer_2.Text = "루시퍼 2";
                Malina.Text = "말리나";
                Modeus.Text = "모데우스";
                Pandemonica.Text = "판데모니카";
                Skeleton.Text = "스켈레톤";
                Zdrada.Text = "즈드라다";

                this.Lucifer_1();
            };
            Lucifer_2.Click += (object o, EventArgs e) => {
                Azazel.Text = "아자젤";
                Cerberus.Text = "케르베로스";
                Helltaker_1.Text = "헬테이커 1";
                Helltaker_2.Text = "헬테이커 2";
                Judgement.Text = "저지먼트";
                Justice.Text = "저스티스";
                Lucifer_1.Text = "루시퍼 1";
                Lucifer_2.Text = ">루시퍼 2";
                Malina.Text = "말리나";
                Modeus.Text = "모데우스";
                Pandemonica.Text = "판데모니카";
                Skeleton.Text = "스켈레톤";
                Zdrada.Text = "즈드라다";

                this.Lucifer_2();
            };
            Malina.Click += (object o, EventArgs e) => {
                Azazel.Text = "아자젤";
                Cerberus.Text = "케르베로스";
                Helltaker_1.Text = "헬테이커 1";
                Helltaker_2.Text = "헬테이커 2";
                Judgement.Text = "저지먼트";
                Justice.Text = "저스티스";
                Lucifer_1.Text = "루시퍼 1";
                Lucifer_2.Text = "루시퍼 2";
                Malina.Text = ">말리나";
                Modeus.Text = "모데우스";
                Pandemonica.Text = "판데모니카";
                Skeleton.Text = "스켈레톤";
                Zdrada.Text = "즈드라다";

                this.Malina();
            };
            Modeus.Click += (object o, EventArgs e) => {
                Azazel.Text = "아자젤";
                Cerberus.Text = "케르베로스";
                Helltaker_1.Text = "헬테이커 1";
                Helltaker_2.Text = "헬테이커 2";
                Judgement.Text = "저지먼트";
                Justice.Text = "저스티스";
                Lucifer_1.Text = "루시퍼 1";
                Lucifer_2.Text = "루시퍼 2";
                Malina.Text = "말리나";
                Modeus.Text = ">모데우스";
                Pandemonica.Text = "판데모니카";
                Skeleton.Text = "스켈레톤";
                Zdrada.Text = "즈드라다";

                this.Modeus();
            };
            Pandemonica.Click += (object o, EventArgs e) => {
                Azazel.Text = "아자젤";
                Cerberus.Text = "케르베로스";
                Helltaker_1.Text = "헬테이커 1";
                Helltaker_2.Text = "헬테이커 2";
                Judgement.Text = "저지먼트";
                Justice.Text = "저스티스";
                Lucifer_1.Text = "루시퍼 1";
                Lucifer_2.Text = "루시퍼 2";
                Malina.Text = "말리나";
                Modeus.Text = "모데우스";
                Pandemonica.Text = ">판데모니카";
                Skeleton.Text = "스켈레톤";
                Zdrada.Text = "즈드라다";

                this.Pandemonica();
            };
            //Skeleton.Click += (object o, EventArgs e) => {
            //    Azazel.Text = "아자젤";
            //    Cerberus.Text = "케르베로스";
            //    Helltaker_1.Text = "헬테이커 1";
            //    Helltaker_2.Text = "헬테이커 2";
            //    Judgement.Text = "저지먼트";
            //    Justice.Text = "저스티스";
            //    Lucifer_1.Text = "루시퍼 1";
            //    Lucifer_2.Text = "루시퍼 2";
            //    Malina.Text = "말리나";
            //    Modeus.Text = "모데우스";
            //    Pandemonica.Text = "판데모니카";
            //    Skeleton.Text = ">스켈레톤";
            //    Zdrada.Text = "즈드라다";

            //    this.Skeleton();
            //};
            Zdrada.Click += (object o, EventArgs e) => {
                Azazel.Text = "아자젤";
                Cerberus.Text = "케르베로스";
                Helltaker_1.Text = "헬테이커 1";
                Helltaker_2.Text = "헬테이커 2";
                Judgement.Text = "저지먼트";
                Justice.Text = "저스티스";
                Lucifer_1.Text = "루시퍼 1";
                Lucifer_2.Text = "루시퍼 2";
                Malina.Text = "말리나";
                Modeus.Text = "모데우스";
                Pandemonica.Text = "판데모니카";
                Skeleton.Text = "스켈레톤";
                Zdrada.Text = ">즈드라다";

                this.Zdrada();
            };

            menu.MenuItems.Add(Lucifer_1);
            menu.MenuItems.Add(Lucifer_2);
            menu.MenuItems.Add(Malina);
            menu.MenuItems.Add(Modeus);
            menu.MenuItems.Add(Skeleton);
            menu.MenuItems.Add(Azazel);
            menu.MenuItems.Add(Justice);
            menu.MenuItems.Add(Judgement);
            menu.MenuItems.Add(Zdrada);
            menu.MenuItems.Add(Cerberus);
            menu.MenuItems.Add(Pandemonica);
            //menu.MenuItems.Add(Helltaker_1);
            //menu.MenuItems.Add(Helltaker_2);
            menu.MenuItems.Add(onTop);
            menu.MenuItems.Add(exit);
            noti.ContextMenu = menu;
        }

        private void Azazel() {
            InitializeComponent();
            original = System.Drawing.Image.FromFile(azazelPath) as Bitmap;
            for (int i = 0; i < 12; i++) {
                frames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(frames[i])) {
                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100),
                        new System.Drawing.Rectangle(i * 100, 0, 100, 100),
                        GraphicsUnit.Pixel);
                }
                var handle = frames[i].GetHbitmap();
                try {
                    imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                } finally {
                    DeleteObject(handle);
                }
            }
        }
        private void Cerberus() {
            InitializeComponent();
            original = System.Drawing.Image.FromFile(cerberusPath) as Bitmap;
            for (int i = 0; i < 12; i++) {
                frames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(frames[i])) {
                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100),
                        new System.Drawing.Rectangle(i * 100, 0, 100, 100),
                        GraphicsUnit.Pixel);
                }
                var handle = frames[i].GetHbitmap();
                try {
                    imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                } finally {
                    DeleteObject(handle);
                }
            }
        }
        //private void Helltaker_1() {
        //    InitializeComponent();
        //    original = System.Drawing.Image.FromFile(helltakerPath) as Bitmap;
        //    for (int i = 0; i < 12; i++) {
        //        frames[i] = new Bitmap(100, 180);
        //        using (Graphics g = Graphics.FromImage(frames[i])) {
        //            g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 180),
        //                new System.Drawing.Rectangle(i * 100, 0, 100, 180),
        //                GraphicsUnit.Pixel);
        //        }
        //        var handle = frames[i].GetHbitmap();
        //        try {
        //            imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
        //                IntPtr.Zero,
        //                Int32Rect.Empty,
        //                BitmapSizeOptions.FromEmptyOptions());
        //        } finally {
        //            DeleteObject(handle);
        //        }
        //    }
        //}
        //private void Helltaker_2() {
        //    InitializeComponent();
        //    original = System.Drawing.Image.FromFile(helltakerPath) as Bitmap;
        //    for (int i = 0; i < 12; i++) {
        //        frames[i] = new Bitmap(100, 100);
        //        using (Graphics g = Graphics.FromImage(frames[i])) {
        //            g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100),
        //                new System.Drawing.Rectangle(i * 100, 300, 100, 100),
        //                GraphicsUnit.Pixel);
        //        }
        //        var handle = frames[i].GetHbitmap();
        //        try {
        //            imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
        //                IntPtr.Zero,
        //                Int32Rect.Empty,
        //                BitmapSizeOptions.FromEmptyOptions());
        //        } finally {
        //            DeleteObject(handle);
        //        }
        //    }
        //}
        private void Judgement() {
            InitializeComponent();
            original = System.Drawing.Image.FromFile(judgementPath) as Bitmap;
            for (int i = 0; i < 12; i++) {
                frames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(frames[i])) {
                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100),
                        new System.Drawing.Rectangle(i * 100, 0, 100, 100),
                        GraphicsUnit.Pixel);
                }
                var handle = frames[i].GetHbitmap();
                try {
                    imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                } finally {
                    DeleteObject(handle);
                }
            }
        }
        private void Justice() {
            InitializeComponent();
            original = System.Drawing.Image.FromFile(justicePath) as Bitmap;
            for (int i = 0; i < 12; i++) {
                frames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(frames[i])) {
                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100),
                        new System.Drawing.Rectangle(i * 100, 0, 100, 100),
                        GraphicsUnit.Pixel);
                }
                var handle = frames[i].GetHbitmap();
                try {
                    imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                } finally {
                    DeleteObject(handle);
                }
            }
        }
        private void Lucifer_1() {
            InitializeComponent();
            original = System.Drawing.Image.FromFile(luciferPath) as Bitmap;
            for (int i = 0; i < 12; i++) {
                frames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(frames[i])) {
                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100),
                        new System.Drawing.Rectangle(i * 100, 0, 100, 100),
                        GraphicsUnit.Pixel);
                }
                var handle = frames[i].GetHbitmap();
                try {
                    imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                } finally {
                    DeleteObject(handle);
                }
            }
        }
        private void Lucifer_2() {
            InitializeComponent();
            original = System.Drawing.Image.FromFile(luciferPath) as Bitmap;
            for (int i = 0; i < 12; i++) {
                frames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(frames[i])) {
                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100),
                        new System.Drawing.Rectangle(i * 100, 100, 100, 100),
                        GraphicsUnit.Pixel);
                }
                var handle = frames[i].GetHbitmap();
                try {
                    imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                } finally {
                    DeleteObject(handle);
                }
            }
        }
        private void Malina() {
            InitializeComponent();
            original = System.Drawing.Image.FromFile(malinaPath) as Bitmap;
            for (int i = 0; i < 12; i++) {
                frames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(frames[i])) {
                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100),
                        new System.Drawing.Rectangle(i * 100, 0, 100, 100),
                        GraphicsUnit.Pixel);
                }
                var handle = frames[i].GetHbitmap();
                try {
                    imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                } finally {
                    DeleteObject(handle);
                }
            }
        }
        private void Modeus() {
            InitializeComponent();
            original = System.Drawing.Image.FromFile(modeusPath) as Bitmap;
            for (int i = 0; i < 12; i++) {
                frames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(frames[i])) {
                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100),
                        new System.Drawing.Rectangle(i * 100, 0, 100, 100),
                        GraphicsUnit.Pixel);
                }
                var handle = frames[i].GetHbitmap();
                try {
                    imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                } finally {
                    DeleteObject(handle);
                }
            }
        }
        private void Pandemonica() {
            InitializeComponent();
            original = System.Drawing.Image.FromFile(pandemonicaPath) as Bitmap;
            for (int i = 0; i < 12; i++) {
                frames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(frames[i])) {
                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100),
                        new System.Drawing.Rectangle(i * 100, 0, 100, 100),
                        GraphicsUnit.Pixel);
                }
                var handle = frames[i].GetHbitmap();
                try {
                    imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                } finally {
                    DeleteObject(handle);
                }
            }
        }
        //private void Skeleton() {
        //    InitializeComponent();
        //    original = System.Drawing.Image.FromFile(skeletonPath) as Bitmap;
        //    for (int i = 0; i < 12; i++) {
        //        frames[i] = new Bitmap(220, 220);
        //        using (Graphics g = Graphics.FromImage(frames[i])) {
        //            g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 220),
        //                new System.Drawing.Rectangle(i * 100, 0, 100, 220),
        //                GraphicsUnit.Pixel);
        //        }
        //        var handle = frames[i].GetHbitmap();
        //        try {
        //            imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
        //                IntPtr.Zero,
        //                Int32Rect.Empty,
        //                BitmapSizeOptions.FromEmptyOptions());
        //        } finally {
        //            DeleteObject(handle);
        //        }
        //    }
        //}
        private void Zdrada() {
            InitializeComponent();
            original = System.Drawing.Image.FromFile(zdradaPath) as Bitmap;
            for (int i = 0; i < 12; i++) {
                frames[i] = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(frames[i])) {
                    g.DrawImage(original, new System.Drawing.Rectangle(0, 0, 100, 100),
                        new System.Drawing.Rectangle(i * 100, 0, 100, 100),
                        GraphicsUnit.Pixel);
                }
                var handle = frames[i].GetHbitmap();
                try {
                    imgFrame[i] = Imaging.CreateBitmapSourceFromHBitmap(handle,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                } finally {
                    DeleteObject(handle);
                }
            }
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left) {
                this.DragMove();
            }
        }

        private void NextFrame(object sender, EventArgs e) {
            frame = ( frame + 1 ) % 12;
            iHelltaker.Source = imgFrame[frame];
        }
    }
}
