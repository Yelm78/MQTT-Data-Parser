using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MQTT_Data_Parser
{
    public partial class Help : Form
    {
        private Form _ownerForm;
        public Help(string exeFullPath, Form ownerForm)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;                // 폼 위치를 모니터 좌측 하단에 설정
            Screen screen = Screen.FromPoint(Cursor.Position);            // 현재 스크린의 작업 영역(작업 표시줄 제외) 가져오기
            Rectangle workingArea = screen.WorkingArea;                   // 작업 영역
            this.Location = new Point(workingArea.Left, workingArea.Top); // 폼 위치 설정

            rtbHelp.Clear();
            AddHelpContents(exeFullPath);
            rtbHelp.SelectionChanged += rtbHelp_SelectionChanged;
            _ownerForm = ownerForm;
        }

        private void AddHelpContents(string exeFullPath)
        {
            // 폼 입력 예시 제목 추가
            RichTextBox_AppendText($"■ 폼 입력 예시", Color.Blue, null, true, false, false, false, 12);
            RichTextBox_AppendText($"\r\n");

            // ConnectServers.json 파일 경로 결정
            string configPath = System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(exeFullPath) ?? "",
                "ConnectServers.json"
            );

            if (!System.IO.File.Exists(configPath))
            {
                RichTextBox_AppendText("ConnectServers.json 파일을 찾을 수 없습니다.\r\n", Color.Red, null, true, false, false, false, 10);
                return;
            }

            // 1. 파일을 줄 단위로 읽어서 토픽별 주석 추출
            var topicCommentMap = new Dictionary<string, string>();
            var fileLines = System.IO.File.ReadAllLines(configPath, Encoding.UTF8);
            foreach (var line in fileLines)
            {
                var trimmed = line.Trim();
                // 토픽 라인 및 주석 포함 여부 확인
                if (trimmed.StartsWith("\"application/") && trimmed.Contains("//"))
                {
                    // "application/31/#",	// 남원요양병원 화재단말
                    var topicName = trimmed.Split('"')[1];
                    var comment = trimmed.Split(new[] { "//" }, 2, StringSplitOptions.None)[1].Trim();
                    topicCommentMap[topicName] = comment;
                }
            }

            // 2. JSON 역직렬화용 주석 제거
            var cleanJsonLines = fileLines
                .Select(line => line.Contains("//") ? line.Split(new[] { "//" }, 2, StringSplitOptions.None)[0] : line)
                .Where(line => !string.IsNullOrWhiteSpace(line));
            string cleanJson = string.Join("\n", cleanJsonLines);

            // 3. 역직렬화
            var options = new JsonSerializerOptions { AllowTrailingCommas = true };
            var servers = System.Text.Json.JsonSerializer.Deserialize<List<MqttServerInfo>>(cleanJson, options) ?? new();

            // 4. RichTextBox에 서버 정보 및 토픽+주석 출력
            foreach (var server in servers)
            {
                // Host 정보 출력
                RichTextBox_AppendText($"  • Host", Color.DimGray, null, true, false, false, false, 10);
                RichTextBox_AppendText($"\r\n");
                RichTextBox_AppendText($"     {server.host}\t");
                RichTextBox_AppendText($" ⇒ {server.description}", Color.Gray, null, false, true, false, false);
                RichTextBox_AppendText($"\r\n");

                // Topic 정보 출력
                RichTextBox_AppendText($"  • Topic", Color.DimGray, null, true, false, false, false, 10);
                RichTextBox_AppendText($" / {server.description}", Color.Gray, null, false, false, false, false);
                RichTextBox_AppendText($"\r\n");

                foreach (var topic in server.topics)
                {
                    string topicName = topic;
                    string topicComment = topicCommentMap.TryGetValue(topicName, out var cmt) ? cmt : "";
                    RichTextBox_AppendText($"     {topicName}\t\t");
                    if (!string.IsNullOrEmpty(topicComment))
                        RichTextBox_AppendText($" ⇒ {topicComment}", Color.Gray, null, false, true, false, false);
                    RichTextBox_AppendText($"\r\n");
                }
            }

            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"  ※ 그 외, 기본적인 내용은 자동완성으로 입력됨.", Color.DimGray, null, true, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"  ※ 기본 네트워크 서버 추가는 별도 요청.", Color.DimGray, null, true, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"\r\n");

            RichTextBox_AppendText($"■ Argument 입력 예시", Color.Blue, null, true, false, false, false, 12);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"\"{exeFullPath}\" ", Color.Gray, null, false, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     --WarningMemoryLevel=", Color.RoyalBlue, null, false, false, false, false);
            RichTextBox_AppendText($"숫자", Color.Red, null, false, true, false, false);
            RichTextBox_AppendText($"(MB 단위)", Color.OrangeRed, null, false, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     --CriticalMemoryLevel=", Color.RoyalBlue, null, false, false, false, false);
            RichTextBox_AppendText($"숫자", Color.Red, null, false, true, false, false);
            RichTextBox_AppendText($"(MB 단위)", Color.OrangeRed, null, false, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     --MaxMemoryUsage=", Color.RoyalBlue, null, false, false, false, false);
            RichTextBox_AppendText($"숫자", Color.Red, null, false, true, false, false);
            RichTextBox_AppendText($"(MB 단위)", Color.OrangeRed, null, false, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     --MinSystemMsgLines=", Color.RoyalBlue, null, false, false, false, false);
            RichTextBox_AppendText($"숫자", Color.Red, null, false, true, false, false);
            RichTextBox_AppendText($"(라인수)", Color.OrangeRed, null, false, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     --MinMqttLines=", Color.RoyalBlue, null, false, false, false, false);
            RichTextBox_AppendText($"숫자", Color.Red, null, false, true, false, false);
            RichTextBox_AppendText($"(라인수)", Color.OrangeRed, null, false, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     --MinParsedLines=", Color.RoyalBlue, null, false, false, false, false);
            RichTextBox_AppendText($"숫자", Color.Red, null, false, true, false, false);
            RichTextBox_AppendText($"(라인수)", Color.OrangeRed, null, false, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     --MaxSystemMsgLines=", Color.RoyalBlue, null, false, false, false, false);
            RichTextBox_AppendText($"숫자", Color.Red, null, false, true, false, false);
            RichTextBox_AppendText($"(라인수)", Color.OrangeRed, null, false, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     --MaxMqttLines=", Color.RoyalBlue, null, false, false, false, false);
            RichTextBox_AppendText($"숫자", Color.Red, null, false, true, false, false);
            RichTextBox_AppendText($"(라인수)", Color.OrangeRed, null, false, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     --MaxParsedLines=", Color.RoyalBlue, null, false, false, false, false);
            RichTextBox_AppendText($"숫자", Color.Red, null, false, true, false, false);
            RichTextBox_AppendText($"(라인수)", Color.OrangeRed, null, false, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     --filePath=", Color.RoyalBlue, null, false, false, false, false);
            RichTextBox_AppendText($"\"", Color.Red, null, false, false, false, false);
            RichTextBox_AppendText($"데이터파일 경로\\", Color.OrangeRed, null, false, true, false, false);
            RichTextBox_AppendText($"FileName", Color.Red, null, false, true, false, false);
            RichTextBox_AppendText($".", Color.OrangeRed, null, false, true, false, false);
            RichTextBox_AppendText($"ext", Color.Red, null, false, false, false, false);
            RichTextBox_AppendText($"\"", Color.Red, null, false, false, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     --Debug=", Color.RoyalBlue, null, false, false, false, false);
            RichTextBox_AppendText($"AnyChar", Color.Gray, null, false, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"  ※ Argument 가 입력된 경우, Debug Mode로 실행 됨.", Color.DimGray, null, true, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     ① 시스템 메시지 자동복사 기능", Color.DimGray, null, true, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     ② 메시지 / 행 수, 메모리, UI 응답성 기록", Color.DimGray, null, true, true, false, false);
            RichTextBox_AppendText($"\r\n");
            RichTextBox_AppendText($"     ③ MQTT 모니터 버퍼 상태 변화(삭제 / 추가) 진단", Color.DimGray, null, true, true, false, false);

            Form_AutoSize(); // 폼사이즈 자동 조정
        }

        // RichTextBox에 내용 추가
        private void RichTextBox_AppendText(
        //RichTextBox_AppendText(text, color, font, isBold, isItalic, isUnderline, isStrikeout, fontSize)
            string text,
            Color color = default,                               // 글꼴 색상, 기본값은 검정색
            Font? font = null,                                   // 글꼴, 기본값은 RichTextBox의 현재 글꼴
            bool blBold = false,                                 // 굵게 여부, 기본값은 false
            bool blItalic = false,                               // 이탤릭 여부, 기본값은 false
            bool blUnderline = false,                            // 밑줄 여부, 기본값은 false
            bool blStrikeout = false,                            // 취소선 여부, 기본값은 false
            float fontSize = 9,                                  // 글꼴 크기, 기본값은 9포인트
            HorizontalAlignment alignment = HorizontalAlignment.Left, // 정렬, 기본값은 왼쪽
            bool blTimeStamp = false                             // 시간표시 여부, 기본값은 false
            ) //정렬, 기본값은 왼쪽
        {
            if (blTimeStamp)
            {
                string timestamp = DateTime.Now.ToString($"yyyy-MM-dd HH:mm:ss.fff"); // 현재 시각을 타임스탬프로 생성
                rtbHelp.AppendText($"{timestamp}\t\t");
            }

            if (color == default(Color)) color = Color.Black;
            if ((font == null) || (font == default)) font = rtbHelp.Font;

            FontStyle style = font.Style;
            if (blBold) style |= FontStyle.Bold;                 // 굵게
            if (blItalic) style |= FontStyle.Italic;             // 이탤릭
            if (blUnderline) style |= FontStyle.Underline;       // 밑줄
            if (blStrikeout) style |= FontStyle.Strikeout;       // 취소선

            Font useFont = (fontSize != 9) ? new Font(font.FontFamily, fontSize, style) : new Font(font, style);

            rtbHelp.SelectionStart = rtbHelp.TextLength;
            rtbHelp.SelectionLength = 0;
            rtbHelp.SelectionColor = color;                      // 색상
            rtbHelp.SelectionFont = useFont;                     // 글꼴
            rtbHelp.SelectionAlignment = alignment;              // 정렬

            if (alignment == HorizontalAlignment.Left)
            { rtbHelp.AppendText(text); }
            else
            { rtbHelp.AppendText(text.EndsWith("\r\n") ? text : text + "\r\n"); }

            rtbHelp.SelectionColor = rtbHelp.ForeColor;          // 기본 글꼴 색상으로 복원
            rtbHelp.SelectionFont = rtbHelp.Font;                // 기본 글꼴로 복원
            rtbHelp.SelectionAlignment = HorizontalAlignment.Left; // 기본 왼쪽 정렬로 복원
        }

        // rtbHelp 텍스트 폭/높이에 맞게 폼과 RichTextBox 크기 자동 조정
        private void Form_AutoSize()
        {
            SuspendLayout();                                     // 폼 레이아웃 일시 중지

            if (string.IsNullOrEmpty(rtbHelp.Text))              // rtbHelp 텍스트가 비어 있으면
            {
                rtbHelp.Text = " ";                              // 빈 문자열 방지용 한 글자 추가
            }

            var originalScrollBars = rtbHelp.ScrollBars;         // 기존 스크롤바 옵션 백업
            var originalWordWrap = rtbHelp.WordWrap;             // 기존 워드랩 옵션 백업
            rtbHelp.ScrollBars = RichTextBoxScrollBars.None;     // 스크롤바 제거(폭 측정 정확도 향상)
            rtbHelp.WordWrap = true;                             // 워드랩 활성화

            int rtbHorizontalPadding = 16;                       // RichTextBox 가로 여유 폭
            int rtbVerticalPadding = 12;                         // RichTextBox 세로 여유 높이
            int minWidth = 260;                                  // 최소 가로폭(가독성)
            int maxWidth = 1200;                                 // 최대 가로폭(모니터 폭)
            int formPadding = 16;                                // 폼 클라이언트 여백

            var lines = rtbHelp.Lines;                           // RichTextBox의 모든 줄 배열
            int maxLineWidth = 0;                                // 최대 줄 폭 초기화
            foreach (var line in lines)
            {
                var size = TextRenderer.MeasureText(
                    line.Length == 0 ? " " : line,               // 빈 줄은 공백으로 대체
                    rtbHelp.Font,
                    new Size(int.MaxValue, int.MaxValue),
                    TextFormatFlags.TextBoxControl | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix
                );
                if (size.Width > maxLineWidth) maxLineWidth = size.Width; // 최대 줄 폭 갱신
            }

            int targetRtbWidth = Math.Min(Math.Max(maxLineWidth + rtbHorizontalPadding, minWidth), maxWidth); // RichTextBox 최종 가로폭 결정

            int originalWidth = rtbHelp.Width;                   // 기존 폭 백업
            rtbHelp.Width = targetRtbWidth;                      // RichTextBox 폭 적용

            rtbHelp.Update();                                    // 레이아웃/그리기 갱신
            int lastIndex = Math.Max(0, rtbHelp.TextLength - 1); // 마지막 문자 인덱스 계산
            var lastPos = rtbHelp.GetPositionFromCharIndex(lastIndex); // 마지막 문자 위치 계산

            int neededRtbHeight = lastPos.Y + rtbHelp.Font.Height + rtbVerticalPadding; // RichTextBox 최종 높이 계산

            int totalClientWidth = targetRtbWidth + formPadding * 2; // 폼 클라이언트 가로 크기 계산
            int totalClientHeight = neededRtbHeight + formPadding * 2; // 폼 클라이언트 세로 크기 계산

            this.ClientSize = new Size(totalClientWidth, totalClientHeight); // 폼 크기 조정

            rtbHelp.Left = formPadding;                          // RichTextBox 좌측 여백 적용
            rtbHelp.Top = formPadding;                           // RichTextBox 상단 여백 적용
            rtbHelp.Width = targetRtbWidth;                      // RichTextBox 폭 적용
            rtbHelp.Height = neededRtbHeight;                    // RichTextBox 높이 적용

            rtbHelp.ScrollBars = originalScrollBars;             // 스크롤바 옵션 원복
            rtbHelp.WordWrap = originalWordWrap;                 // 워드랩 옵션 원복
            ResumeLayout(performLayout: true);                   // 폼 레이아웃 재개
        }

        // 선택 영역이 바뀔 때마다 클립보드로 복사
        private void rtbHelp_SelectionChanged(object? sender, EventArgs e)
        {
            if ((chkCopytoClipBoard.Checked) && (!string.IsNullOrEmpty(rtbHelp.SelectedText)))
            {
                Clipboard.SetText(rtbHelp.SelectedText);         // 선택된 텍스트를 클립보드에 복사
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (_ownerForm != null)
            {
                _ownerForm.Activate();                           // 원래 폼 활성화
            }
        }

        public class MqttServerInfo
        {
            public string host { get; set; } = "";
            public int port { get; set; }
            public string userName { get; set; } = "";
            public string password { get; set; } = "";
            public List<string> topics { get; set; } = new();
            public string description { get; set; } = "";
        }
    }
}