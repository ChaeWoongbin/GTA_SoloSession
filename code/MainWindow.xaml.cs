using System.Windows;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Threading;

namespace restartProcess
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private int time = 10;
        private DispatcherTimer Timer;
        Process[] getProcess;
        public MainWindow()
        {
            InitializeComponent();

            main_.Text = "\n-------\n";
            main_.Text += "start시 프로세스가 일시정지 됩니다..\n";
            main_.Text += "1. 프로세스가 일시정지 됩니다..\n";
            main_.Text += "2. 프로세스를 재기동 합니다.\n";
            main_.Text += "3. 해당 프로세스를 윈도우에 활성화합니다.\n";
            main_.Text += "4. 이 프로그램은 작업 후 자동종료 됩니다.\n";
            main_.Text += "\n-------\n";
            main_.Text += "Process pause : 10sec \n";
            main_.Text += "Program Ver : 0.2v\n";
            main_.Text += "release : 2019.05.14\n";
            main_.Text += "\n-------\n";
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr handle, int nCmdShow);

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main_.Text = "start...\n\n";

            while (true)
            {
                getProcess = Process.GetProcessesByName("GTA5"); // 프로세스가져오기 ( 리소스 모니터 exe 제외한 이름 ) // notepad (메모장) test중
                if (getProcess.Length < 1)
                {
                    main_.Text = "\n프로세스가 실행 중이 아닙니다.(GTA.exe)\n";
                    Delay(1500);
                    main_.Text = "\n----- Explanation ------\n";
                    main_.Text += "start시 프로세스가 일시정지 됩니다..\n";
                    main_.Text += "1. 프로세스가 일시정지 됩니다..\n";
                    main_.Text += "2. 프로세스를 재기동 합니다.\n";
                    main_.Text += "3. 해당 프로세스를 윈도우에 활성화합니다.\n";
                    main_.Text += "4. 이 프로그램은 작업 후 자동종료 됩니다.\n";
                    main_.Text += "\n------- Setting -------\n";
                    main_.Text += "Timer : 10sec \n";
                    main_.Text += "Ver : 0.2\n";
                    main_.Text += "release : 2019.05.14\n";
                    main_.Text += "\n-----------------------\n";
                    break;
                }
                else
                {
                    getProcess[0].Suspend(); // 프로세스 일시정지
                    main_.Text += "프로세스 정지중..";
                    count_tb.Text = "10";
                    time = 10; // 카운트다운 시작
                    Timer = new DispatcherTimer();
                    Timer.Interval = new TimeSpan(0, 0, 1);
                    Timer.Tick += Timer_Tick;
                    Timer.Start();
                    /*
                    Delay(10000);

                    main_.Text += "\n\n프로세스 재기동..\n\n";
                    getProcess[0].Resume(); // 프로세스 다시실행

                    Delay(1000);
                    main_.Text += "프로세스 재기동 완료..\n\n";

                    Delay(1000);
                    main_.Text += "프로세스가 활성화 되면 종료됩니다.\n\n";
                    for (int i = 0; i < getProcess.Length; i++){ // 최소화된 창 올리기
                        ShowWindow(getProcess[i].MainWindowHandle, 9);
                        SetForegroundWindow(getProcess[i].MainWindowHandle);
                    }
                    */

                    break;
                }
            }            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                time--;
                count_tb.Text = string.Format("0{0}", time % 60);
                main_.Text += " .";
            }
            else
            {
                Timer.Stop(); // 카운트다운 종료

                main_.Text += "\n\n프로세스 재기동..\n\n";
                getProcess[0].Resume(); // 프로세스 다시실행

                Delay(1000);
                main_.Text += "프로세스 재기동 완료..\n\n";

                Delay(1000);
                main_.Text += "프로세스가 활성화 되면 종료됩니다.\n\n";
                for (int i = 0; i < getProcess.Length; i++)
                { // 최소화된 창 올리기
                    ShowWindow(getProcess[i].MainWindowHandle, 9);
                    SetForegroundWindow(getProcess[i].MainWindowHandle);
                }
                                
                Close();
            }
        }

        public static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                if (System.Windows.Application.Current != null)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new Action(delegate { }));
                }
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // 프로그램 종료
        {
            Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) // 창 이동
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }
    }
}
