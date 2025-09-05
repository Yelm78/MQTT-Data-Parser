///<remarks>
///⚠ 주석 작성 규칙(필수) ⚠
///  1. 주석의 형식 및 위치
///     • region 영역은 주석을 사용하지 않음(기존 유지)
///     • 메서드/필드/지역변수 선언부 상단:
///        - 한 줄로 역할을 간결하게 요약
///        - // 한 줄 주석 사용
///        - 본문 코드내에서는 <summary> 등 XML 주석 사용 안 함
///     • 쉽고, 보편적인 코드를 사용하여 누구나 바로 알 수 있는 부분은 주석 생략
///     • 코드 라인 옆:
///        - 가능한 모든 코드 행에 주석 작성(메소드 제목 제외)
///        - 기존 주석처리된 코드는 유지(삭제 및 수정 금지)
///        - 주석의 정렬 및 가독성 규칙에 따라 코드와 주석 사이에 공백을 두고, 코드 오른쪽에 한 줄 주석
///        - 각 변수 선언, 주요 로직, 제어 흐름은 시작 상단에 해당 코드의 목적, 동작, 효과를 한 줄로 요약
///        - 조건문, 분기문의 시작부분 상단에 주석 작성
///        - 여러 줄 설명이 필요한 경우에도 한 줄씩 나누어 각 코드 라인에 배치
///  2. 주석의 문체 및 어투
///     • 명령문 사용:
///        - "합니다", "하세요", "함" 등 존댓말, 구어체, 감탄문 없음 
///     • 간결하고 명확한 문장:
///        - 서술어, 수식어, 장황한 설명 없이 객관적 기술
///        - "~를", "~할", "~하고" 등 없이 핵심 요약 기술
///        - 예시:
///          // UI 스레드 실행
///          // 최대 라인 수 초과 시 앞부분 삭제
///          // 예외 발생 : 시스템 메시지 알림
///  3. 주석의 내용
///     • 역할/목적 설명:
///        - 메서드 / 필드 / 블록의 상단 주석은 해당 코드의 목적, 동작, 효과를 한 줄로 요약
///        - 예시:
///          // 입력란 에러시 배경색 변경, 에러 여부 반환
///          // 폼 및 주요 UI 컨트롤, 이벤트, 타이머 초기화, 하단 메시지 입력란 상태 설정
///     • 로직 흐름/제어 흐름/예외 처리:
///     • 조건문, 반복문, 예외 처리 등 제어 흐름마다 주석으로 의도와 분기 설명
///     • 변수 / 상수 / 필드 설명:
///        - 각 변수 선언에 해당 변수의 용도, 단위, 초기값 의미 등 명확히 기술
///        - 예시:
///          private const long WarningMemoryLevel = 128/*MB*/ * (1024 * 1024); // 메모리 경고 임계치
///          private int marqueeSpeed = 4; // 하단 이동 속도(픽셀)
///  4. 주석의 정렬 및 가독성(필수)
///     • 코드별 주석의 시작 위치 통일:
///        - 행의 처음 문자 위치(1칸)에서 주석 시작(//)까지 50~90칸으로 맞춰 가독성 높임
///          (메소드 내 모든 주석 시작위치는 동일하게 유지. 코드가 길어 90칸을 초과하는 경우, 해당 행은 최소 1칸)
///     • 코드와 주석 사이 일정 간격 유지:
///        - 여러 줄 주석이 필요한 경우에도 각 줄마다 코드 옆에 정렬
///  5. 기타 특징
///     • 중복/불필요한 주석 없음:
///        - 코드 자체로 명확한 부분에는 주석 생략(try, catch, return 등)
///        - 주석이 코드의 동작, 목적, 예외 상황을 정확히 반영하도록 작성
///     • 영문/한글 혼용:
///        - 변수명, 상수명 등은 영문, 주석은 한글로 통일
///        - 단위, 예시 등은 필요시 괄호로 보충 설명
///</remarks>

using MQTTnet;
using MQTTnet.Client;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;
using System.Text.Encodings.Web;
using System.Runtime.CompilerServices;
//using System.Drawing;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Intrinsics.Arm;
//using static System.Net.Mime.MediaTypeNames;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
//using System;
//using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
//using System.Reflection.Emit;
//using System.Drawing.Text;
//using System.Diagnostics.Eventing.Reader;


namespace MQTT_Data_Parser
{
    // MQTTDataParser: WinForms 앱. MQTT 브로커와 통신, 메시지 수신/파싱/저장/표시.
    public partial class MQTTDataParser : Form
    {

        #region 상수 및 변수

        // [1. 폼 생성 및 UI 이벤트 관련 변수]
        private Color defaultInputBackColor;                                             // 입력란 기본 배경색
        private int ScrollSpeed = 4;                                                     // 하단 이동 속도(픽셀)
        private int ScrollDirection = -1;                                                // 하단 이동 방향(-1: 왼쪽, 1: 오른쪽)
        private readonly string[]? _startupArgs;                                         // 시작 인자(명령줄 인자)
        private Help? helpFormInstance;                                                  // 도움말 폼 인스턴스

        // [2. 로그/메시지 관련 변수]
        private readonly List<string> SystemMsgMessages = new();                         // 시스템 메시지 리스트
        private readonly List<List<(string Text, Color Color)>> mqttColorMessages = new(); // MQTT 모니터 색상 메시지 버퍼
        private readonly List<string> LogMessages = new();                                 // 파싱 데이터 메시지 리스트
        private readonly List<List<Dictionary<string, string>>> pendingParsedRows = new(); // 파싱 데이터 버퍼
        private readonly List<string[]> pendingParsedHeaders = new();                    // 파싱 헤더 버퍼
        private readonly List<(string topic, string payload)> monitorMsgBuffer = new();  // MQTT 모니터 메시지 버퍼
        private readonly object monitorMsgLock = new();                                  // 모니터 메시지 버퍼 락 오브젝트

        // [3. MQTT 연결/이벤트 관련 변수]
        private IMqttClient? mqttClient;                                                 // MQTT 클라이언트 인스턴스
        private bool isConnected = false;                                                // MQTT 연결 상태
        private MqttClientOptions? lastMqttOptions = null;                               // 마지막 연결 옵션
        private string? lastMqttTopic = null;                                            // 마지막 구독 토픽
        private bool isManualDisconnect = false;                                         // 수동 연결 해제 여부
        private CancellationTokenSource? reconnectCts;                                   // 재연결 취소 토큰
        int MQTTretryDelayMs = 3000;                                                     // 재연결 대기(ms)
        int MQTTmaxRetry = 28800;                                                        // 재시도 횟수
        private bool isReconnecting = false;                                             // 재연결 중 플래그
        private List<MqttServerInfo> serverList = new();                                 // 서버 접속정보 리스트

        // [4. 파싱/데이터 처리 관련 변수]
        private bool isHeaderWritten = false;                                            // 파싱 데이터 헤더 출력 여부
        private List<string> lastHeaderFields = new();                                   // 마지막 출력 헤더 필드명 목록
        private List<string> dynamicHeaders = new();                                     // 동적 헤더 목록
        private List<Dictionary<string, string>> parsedRows = new();                     // 파싱된 데이터 행 목록
        private bool _syncingPanels = false;                                             // 패널 동기화 중 플래그
        private readonly SemaphoreSlim _parsingSemaphore = new SemaphoreSlim(4);         // 동시 파싱 작업 제한
        private readonly ConcurrentQueue<string> _parsingQueue = new ConcurrentQueue<string>(); // 파싱 작업 큐
        private Task? _parsingWorkerTask = null;                                         // 파싱 워커 태스크
        private CancellationTokenSource _parsingWorkerCts = new CancellationTokenSource(); // 파싱 워커 취소 토큰

        // [5. 메모리/리소스 관리 관련 변수]
        private readonly long WarningMemoryLevel = 128/*MB*/ * (1024 * 1024);            // 메모리 경고 임계치(128MB)
        private readonly long CriticalMemoryLevel = 256/*MB*/ * (1024 * 1024);           // 메모리 임계 임계치(256MB)
        private readonly long MaxMemoryUsage = 512/*MB*/ * (1024 * 1024);                // 최대 메모리 사용량(512MB)
        private readonly long MaxLogFileSize = 1/*MB*/ * (1024 * 1024);                  // 로그 파일 최대 크기(1MB)
        private System.Threading.Timer? memoryTimer;                                     // 메모리 모니터링 타이머
        private int memoryTimerTick = 10000;                                             // 메모리 타이머 Tick 간격(ms)
        private int _memoryTickRunning = 0;                                              // 메모리 체크 중복 실행 방지 플래그
        private DateTime ResetTime = DateTime.MinValue;                                  // 마지막 메모리 체크 시간
        private int ResetCount = 0;                                                      // 메모리 체크 카운트
        private long cls = 3600;                                                         // 컨트롤 재생성 주기(초)

        // [공통: 파일/저장/큐 관련 변수]
        private string exePath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ?? string.Empty) ?? string.Empty; // 실행 파일 경로
        private StreamWriter? fileWriter = null;                                         // 파싱 데이터 파일 Writer
        private string? ParsingDataFilePath = null;                                      // 파싱 데이터 파일 경로
        private readonly ConcurrentQueue<string> fileWriteQueue = new();                 // 파일 저장 큐
        private CancellationTokenSource? fileWriteCts;                                   // 파일 저장 취소 토큰
        private readonly object fileWriteLock = new();                                   // 파일 쓰기/읽기 락 오브젝트

        // [공통: UI/타이머/상태 관련 변수]
        private System.Threading.Timer? marqueeTimer;                                    // 하단 애니메이션 타이머
        private System.Windows.Forms.Timer? uiUpdateTimer;                               // UI 갱신 타이머
        private readonly System.Threading.Timer _uiUpdateThrottleTimer;                  // UI 업데이트 스로틀 타이머
        private volatile bool _uiUpdatePending = false;                                  // UI 업데이트 대기 플래그
        private DateTime _lastUIUpdate = DateTime.MinValue;                              // 마지막 UI 업데이트 시간

        // [공통: 라인 제한/설정 관련 변수]
        private readonly int MinSystemMsgLines = 100;                                    // 시스템 메시지 최소 라인 제한
        private readonly int MinMqttLines = 500;                                         // MQTT 모니터 최소 라인 제한
        private readonly int MinParsedLines = 50;                                        // 파싱 데이터 최소 라인 제한
        private readonly int MaxSystemMsgLines = 200;                                    // 시스템 메시지 최대 라인 제한
        private readonly int MaxMqttLines = 1000;                                        // MQTT 모니터 최대 라인 제한
        private readonly int MaxParsedLines = 500;                                       // 파싱 데이터 최대 라인 제한
        private int systemMsgLimit;                                                      // 시스템 메시지 현재 라인 제한
        private int mqttLimit;                                                           // MQTT 모니터 현재 라인 제한
        private int parsedLimit;                                                         // 파싱 데이터 현재 라인 제한

        // [공통: 명령줄 인자/디버그 플래그]
        private readonly bool argsParsed = false;                                        // 명령줄 인자 파싱 여부
        private readonly bool argsParsedWarningMemoryLevel = false;                      // WarningMemoryLevel 인자 파싱 여부
        private readonly bool argsParsedCriticalMemoryLevel = false;                     // CriticalMemoryLevel 인자 파싱 여부
        private readonly bool argsParsedMaxMemoryUsage = false;                          // MaxMemoryUsage 인자 파싱 여부
        private readonly bool argsParsedMaxLogFileSize = false;                          // MaxLogFileSize 인자 파싱 여부
        private readonly bool argsParsedMinSystemMsgLines = false;                       // MinSystemMsgLines 인자 파싱 여부
        private readonly bool argsParsedMinMqttLines = false;                            // MinMqttLines 인자 파싱 여부
        private readonly bool argsParsedMinParsedLines = false;                          // MinParsedLines 인자 파싱 여부
        private readonly bool argsParsedMaxSystemMsgLines = false;                       // MaxSystemMsgLines 인자 파싱 여부
        private readonly bool argsParsedMaxMqttLines = false;                            // MaxMqttLines 인자 파싱 여부
        private readonly bool argsParsedMaxParsedLines = false;                          // MaxParsedLines 인자 파싱 여부
        private readonly bool argsParsedfilePath = false;                                // filePath 인자 파싱 여부
        private readonly bool argsDebug = false;                                         // Debug 인자 파싱 여부

        // [공통: 기타]
        private readonly string serverConfigPath = (System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ?? string.Empty) ?? string.Empty) + "\\ConnectServers.json"; // 설정 파일 경로
        private static readonly object diagLogLock = new();                              // 진단 로그 락 오브젝트

        // [공통: 시스템 절전 방지 관련 상수]
        [DllImport("kernel32.dll")] private static extern uint SetThreadExecutionState(uint esFlags); // 시스템 절전 방지 Win32 API
        private const uint ES_CONTINUOUS = 0x80000000;                                   // 절전 방지 플래그(계속 유지)
        private const uint ES_DISPLAY_REQUIRED = 0x00000002;                             // 디스플레이 절전 방지 플래그
        private const uint ES_SYSTEM_REQUIRED = 0x00000001;                              // 시스템 절전 방지 플래그

        // [공통: 취소 토큰]
        private CancellationTokenSource? systemMsgCts;                                   // 시스템 메시지 정리 취소 토큰
        private CancellationTokenSource? mqttMonitorCts;                                 // MQTT 모니터 정리 취소 토큰
        private CancellationTokenSource? parsingDataCts;                                 // 파싱 데이터 정리 취소 토큰

        #endregion



        #region 1. 폼 생성 / UI 이벤트

        // 폼 및 주요 UI 컨트롤, 이벤트, 타이머 초기화 등
        public MQTTDataParser() : this(null) { }
        // Update the constructor to initialize the MinSize field
        public MQTTDataParser(string[]? args)
        {
            InitializeComponent();                                                         // WinForms 컨트롤 및 레이아웃 초기화


            this.StartPosition = FormStartPosition.Manual;                                 // 폼 위치를 모니터 좌측 하단에 설정
            Screen screen = Screen.FromPoint(Cursor.Position);                             // 현재 스크린의 작업 영역(작업 표시줄 제외) 가져오기
            Rectangle workingArea = screen.WorkingArea;                                    // 작업 영역 계산
            this.Location = new Point(workingArea.Left, workingArea.Bottom - this.Height); // 폼 위치 설정

            dgvParsingData.CellValueNeeded += OnGridCellValueNeeded;                       // 셀 값 요청 시 핸들러 등록

            AutoComplete_Load();                                                           // 설정 파일에서 자동완성 목록 로딩
            AutoComplete();                                                                // 자동완성 설정
            LineLabels_Update();                                                           // 라벨 텍스트 초기화
            ConnectionUI_Update(false);

            systemMsgLimit = MaxSystemMsgLines;
            mqttLimit = MaxMqttLines;
            parsedLimit = MaxParsedLines;

            _uiUpdateThrottleTimer = new System.Threading.Timer(_ =>
            {
                if (_uiUpdatePending && !this.IsDisposed)
                {
                    RunOnUIThread(this, LineLabels_Update);
                }
            }, null, 0, 200);                                                               // 200ms 간격으로 체크
        }

        // 서버 목록 파일에서 서버 정보 리스트 읽어 반환
        private List<MqttServerInfo> ServerListFile_Load()
        {
            try
            {
                if (File.Exists(serverConfigPath))                           // 서버 설정 파일 존재 여부 확인
                {
                    return ServerList_Load();                                // 파일 존재 시 서버 정보 리스트 반환
                }
                else
                {
                    ServerListFile_Create();                                 // 파일 없으면 기본 서버 정보 파일 생성
                    tbPassword.PasswordChar = '\0';                          // 비밀번호 입력란 마스킹 해제
                    return ServerList_Load();                                // 생성 후 서버 정보 리스트 반환
                }
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"ServerListFile_Load() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
                return new();                                                // 예외 발생 시 빈 리스트 반환
            }
        }

        // 서버 목록 파일에서 서버 정보 리스트 읽어 반환(실제 구동부분)
        private List<MqttServerInfo> ServerList_Load()
        {
            var lines = File.ReadAllLines(serverConfigPath)                  // 파일 전체 줄 단위로 읽기
                        .Select(line => line.Contains("//") ? line.Split(new[] { "//" }, 2, StringSplitOptions.None)[0] : line) // 인라인 주석 제거
                        .Where(line => !string.IsNullOrWhiteSpace(line))     // 빈 줄 제거
                        .ToArray();
            var json = string.Join("\n", lines);                             // 줄 배열을 다시 JSON 문자열로 결합
            return System.Text.Json.JsonSerializer.Deserialize<List<MqttServerInfo>>(json) ?? new(); // JSON 역직렬화, 실패 시 빈 리스트 반환
        }

        // 서버 목록 파일이 없으면 기본 서버 정보로 파일 생성
        private void ServerListFile_Create()
        {
            if (File.Exists(serverConfigPath)) return;                       // 파일이 이미 존재하면 함수 종료

            // 기본 서버 정보 JSON(주석 포함, UTF-8 인코딩)
            var defaultJson = @"[
  {
    ""host"": ""IP 또는 URL"", // 첫눈 클라우드 서버
    ""port"": 1883,
    ""userName"": ""ID 입력"",
    ""password"": ""PW 입력"",
    ""topics"": [
      ""application/#"",    // 전체(Topic 설명)
      ""application/1/#"",  // App1(Topic 설명)
    ],
    ""description"": ""ㅇㅇ 네트워크 서버""
  },
  {
    ""host"": ""IP 또는 URL"", // ?? 네트워크 서버
    ""port"": 1883,
    ""userName"": ""ID 입력"",
    ""password"": ""PW 입력"",
    ""topics"": [
      ""application/#"",    // 전체(Topic 설명)
      ""application/1/#"",  // App1(Topic 설명)
    ],
    ""description"": ""ㅁㅁ 네트워크 서버""
  }
]";
            File.WriteAllText(serverConfigPath, defaultJson, Encoding.UTF8); // 파일 생성 및 내용 기록(UTF-8)
        }

        // 서버 정보 추가/수정(서버별 토픽 주석 인라인 유지, 주석 뒤섞임 방지)
        private void ServerList_Add_Update(string host, int port, string user, string pass, string topic, string description = "")
        {
            var lines = File.ReadAllLines(serverConfigPath).ToList();                  // 서버 설정 파일 전체 줄 읽기

            var serverTopicComments = new Dictionary<(int serverIdx, string topic), string>(); // 서버별 토픽 주석 매핑 딕셔너리
            int serverIdx = -1;                                                        // 서버 인덱스 초기화
            foreach (var line in lines)                                                // 파일의 각 줄 순회
            {
                var trimmed = line.Trim();                                             // 앞뒤 공백 제거
                if (trimmed.StartsWith("{")) serverIdx++;                              // 서버 객체 시작 시 인덱스 증가
                if (trimmed.StartsWith("\"application/") && trimmed.Contains("//"))    // 토픽 라인 및 주석 포함 여부 확인
                {
                    var topicName = trimmed.Split('"')[1];                             // 토픽명 추출
                    var comment = "//" + trimmed.Split(new[] { "//" }, 2, StringSplitOptions.None)[1]; // 주석 추출
                    serverTopicComments[(serverIdx, topicName)] = comment;             // 서버 인덱스+토픽명으로 주석 저장
                }
            }

            var jsonLines = lines
                .Select(line => line.Contains("//") ? line.Split(new[] { "//" }, 2, StringSplitOptions.None)[0] : line) // 인라인 주석 제거
                .Where(line => !string.IsNullOrWhiteSpace(line))                       // 빈 줄 제거
                .ToList();

            var json = string.Join("\n", jsonLines);                                   // JSON 문자열 결합
            var servers = System.Text.Json.JsonSerializer.Deserialize<List<MqttServerInfo>>(json) ?? new(); // JSON 역직렬화

            var server = servers.FirstOrDefault(x => x.host == host);                  // 호스트로 서버 정보 조회
            if (server == null)                                                        // 서버 정보 없으면
            {
                server = new MqttServerInfo
                {
                    host = host,                                                       // 호스트 저장
                    port = port,                                                       // 포트 저장
                    userName = user,                                                   // 사용자명 저장
                    password = pass,                                                   // 비밀번호 저장
                    topics = new List<string> { topic },                               // 토픽 리스트 생성
                    description = description                                          // 설명 저장
                };
                servers.Add(server);                                                   // 서버 정보 추가
            }
            else                                                                       // 서버 정보 있으면
            {
                if (server.userName != user) server.userName = user;                   // 사용자명 갱신
                if (server.password != pass) server.password = pass;                   // 비밀번호 갱신
                if (!server.topics.Contains(topic)) server.topics.Add(topic);          // 토픽 추가
                if (!string.IsNullOrEmpty(description)) server.description = description; // 설명 갱신
            }

            var options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,                                                  // 들여쓰기 옵션
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping                  // 인코딩 옵션
            };
            var newJson = System.Text.Json.JsonSerializer.Serialize(servers, options); // 서버 정보 직렬화

            var newLines = newJson.Split('\n');                                        // 직렬화 결과 줄 단위 분리
            serverIdx = -1;                                                            // 서버 인덱스 재초기화
            foreach (var (line, i) in newLines.Select((line, i) => (line, i)))         // 각 줄 순회
            {
                var trimmed = line.TrimStart();                                        // 앞쪽 공백 제거
                if (trimmed.StartsWith("{")) serverIdx++;                              // 서버 객체 시작 시 인덱스 증가
                if (trimmed.StartsWith("\"application/"))                              // 토픽 라인 확인
                {
                    var topicName = trimmed.Split('"')[1];                             // 토픽명 추출
                    if (serverTopicComments.TryGetValue((serverIdx, topicName), out var comment)) // 서버별 토픽 주석 조회
                    {
                        string baseLine = line.TrimEnd('\r', '\n', ' ', '\t');         // 줄 끝 공백 제거
                        newLines[i] = baseLine + "\t" + comment;                       // 인라인 주석 복원
                    }
                }
            }

            File.WriteAllLines(serverConfigPath, newLines, Encoding.UTF8);             // 파일에 결과 저장
        }

        // 서버 목록 파일에서 자동완성 목록 및 계정 정보 동적 로딩
        private void AutoComplete_Load()
        {
            serverList = ServerListFile_Load();                                  // 서버 목록 파일에서 서버 정보 리스트 로딩

            var hostAutoComplete = new AutoCompleteStringCollection();           // 호스트 자동완성 컬렉션 생성
            hostAutoComplete.AddRange(serverList.Select(s => s.host).ToArray()); // 서버 목록에서 호스트 주소 추출하여 자동완성 소스에 추가
            tbHost.AutoCompleteCustomSource = hostAutoComplete;                  // 호스트 입력란에 자동완성 소스 지정

            tbHost.TextChanged += (s, e) =>
            {
                var host = tbHost.Text.Trim();                                   // 입력된 호스트 주소 공백 제거
                var server = serverList.FirstOrDefault(x => x.host == host);     // 입력값과 일치하는 서버 정보 조회
                var topicAutoComplete = new AutoCompleteStringCollection();      // 토픽 자동완성 컬렉션 생성

                if (server != null)                                              // 서버 정보가 존재하면
                {
                    topicAutoComplete.AddRange(server.topics.ToArray());         // 해당 서버의 토픽 목록 자동완성 소스에 추가
                    tbPort.Text = server.port.ToString();                        // 서버 포트 입력란에 값 자동 입력
                    tbUserName.Text = server.userName;                           // 서버 계정 입력란에 값 자동 입력
                    tbPassword.Text = server.password;                           // 서버 비밀번호 입력란에 값 자동 입력
                    tbTopic.Text = server.topics.FirstOrDefault() ?? "";         // 첫 번째 토픽을 토픽 입력란에 자동 입력
                    lblHost.Text = server.description;                           // 서버정보를 호스트 라벨에 표시

                    var topicComment = "";                                       // 토픽 주석 추출
                    var lines = System.IO.File.ReadAllLines(serverConfigPath, Encoding.UTF8);
                    foreach (var line in lines)
                    {
                        var trimmed = line.Trim();
                        if (trimmed.StartsWith($"\"{tbTopic.Text}\"") && trimmed.Contains("//"))
                        {
                            topicComment = trimmed.Split(new[] { "//" }, 2, StringSplitOptions.None)[1].Trim();
                            break;
                        }
                    }
                    lblTopic.Text = topicComment;                                // 토픽 주석을 토픽 라벨에 표시
                }
                else                                                             // 서버 정보가 없으면
                {
                    topicAutoComplete.AddRange(serverList.SelectMany(x => x.topics).Distinct().ToArray()); // 전체 서버의 토픽 목록을 자동완성 소스에 추가
                    lblHost.Text = "Host";                                       // 호스트 라벨 기본값 설정
                    lblTopic.Text = "Topic";                                     // 토픽 라벨 기본값 설정
                }
                tbTopic.AutoCompleteCustomSource = topicAutoComplete;            // 토픽 입력란에 자동완성 소스 지정
            };
        }

        // 서버 목록 기반 호스트/토픽 자동완성 및 계정 정보 동적 설정
        private void AutoComplete()
        {
            var hostAutoComplete = new AutoCompleteStringCollection();           // 호스트 자동완성 컬렉션 생성
            hostAutoComplete.AddRange(serverList.Select(s => s.host).ToArray()); // 서버 목록에서 호스트 주소 추출하여 자동완성 소스에 추가
            tbHost.AutoCompleteMode = AutoCompleteMode.SuggestAppend;            // 인라인+목록 자동완성 모드 설정
            tbHost.AutoCompleteSource = AutoCompleteSource.CustomSource;         // 사용자 지정 소스 사용
            tbHost.AutoCompleteCustomSource = hostAutoComplete;                  // 호스트 자동완성 소스 지정
            tbTopic.AutoCompleteMode = AutoCompleteMode.SuggestAppend;           // 토픽 입력란 자동완성 모드 설정
            tbTopic.AutoCompleteSource = AutoCompleteSource.CustomSource;        // 사용자 지정 소스 사용

            tbHost.TextChanged += (s, e) =>
            {
                var host = tbHost.Text.Trim();                                   // 입력된 호스트 주소 공백 제거
                var server = serverList.FirstOrDefault(x => x.host == host);     // 입력값과 일치하는 서버 정보 조회
                var topicAutoComplete = new AutoCompleteStringCollection();      // 토픽 자동완성 컬렉션 생성

                if (server != null)                                              // 서버 정보가 존재하면
                {
                    topicAutoComplete.AddRange(server.topics.ToArray());         // 해당 서버의 토픽 목록 자동완성 소스에 추가
                    tbPort.Text = server.port.ToString();                        // 서버 포트 입력란에 값 자동 입력
                    tbUserName.Text = server.userName;                           // 서버 계정 입력란에 값 자동 입력
                    tbPassword.Text = server.password;                           // 서버 비밀번호 입력란에 값 자동 입력
                    tbTopic.Text = server.topics.FirstOrDefault() ?? "";         // 첫 번째 토픽을 토픽 입력란에 자동 입력
                    lblHost.Text = server.description;                           // 서버정보를 호스트 라벨에 표시

                    var topicComment = "";                                       // 토픽 주석 추출
                    var lines = System.IO.File.ReadAllLines(serverConfigPath, Encoding.UTF8);
                    foreach (var line in lines)
                    {
                        var trimmed = line.Trim();
                        if (trimmed.StartsWith($"\"{tbTopic.Text}\"") && trimmed.Contains("//"))
                        {
                            topicComment = trimmed.Split(new[] { "//" }, 2, StringSplitOptions.None)[1].Trim();
                            break;
                        }
                    }
                    lblTopic.Text = topicComment;                                // 토픽 주석을 토픽 라벨에 표시
                }
                else                                                             // 서버 정보가 없으면
                {
                    topicAutoComplete.AddRange(serverList.SelectMany(x => x.topics).Distinct().ToArray()); // 전체 서버의 토픽 목록을 자동완성 소스에 추가
                    lblHost.Text = "Host";                                       // 호스트 라벨 기본값 설정
                    lblTopic.Text = "Topic";                                     // 토픽 라벨 기본값 설정
                }
                tbTopic.AutoCompleteCustomSource = topicAutoComplete;            // 토픽 입력란에 자동완성 소스 지정
            };
        }

        // 문자열을 MB 단위 long(바이트)로 변환, 변환 실패 시 0 반환
        private long ParseMB(string value)
        {
            if (long.TryParse(value, out var mb))        // 문자열을 long으로 변환 시도
                return mb * 1024 * 1024;                 // MB 단위를 바이트로 변환하여 반환
            return 0;                                    // 변환 실패 시 0 반환
        }

        // 문자열을 int로 변환, 변환 실패 시 0 반환
        private int ParseInt(string value)
        {
            if (int.TryParse(value, out var i))          // 문자열을 int로 변환 시도
                return i;                                // 변환 성공 시 값 반환
            return 0;                                    // 변환 실패 시 0 반환
        }

        // 지정 컨트롤 UI 스레드 액션 실행
        private void RunOnUIThread(Control ctrl, Action action)
        {
            try
            {
                if (!ctrl.IsHandleCreated)              // 컨트롤의 핸들이 아직 생성되지 않은 경우
                    ctrl.CreateControl();               // 핸들(윈도우 리소스) 생성

                if (ctrl.InvokeRequired)                // 현재 스레드가 UI 스레드가 아닌 경우
                    ctrl.Invoke(action);                // UI 스레드에서 액션 실행(동기)
                else
                    action();                           // 이미 UI 스레드면 바로 액션 실행
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"RunOnUIThread(Control ctrl, Action action) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
            }
        }

        // 연결상태에 따라 주요 입력란/버튼/라벨 색상 및 속성 일괄 갱신
        private void ConnectionUI_Update(bool connected)
        {
            if (this.InvokeRequired) { this.Invoke(new Action(() => ConnectionUI_Update(connected))); return; } // UI 스레드에서 재호출

            isConnected = connected;                                                          // 연결상태 플래그 갱신

            Color backColor = connected ? grbConnection.BackColor : SystemColors.Window;      // 연결상태에 따라 배경색 선택
            Color foreColor = connected ? SystemColors.HotTrack : SystemColors.WindowText;    // 연결상태에 따라 텍스트색 선택
            Color lblForeColor = connected ? SystemColors.GrayText : SystemColors.WindowText; // 라벨 텍스트색 선택

            // 입력란/버튼/라벨 색상 및 속성 일괄 적용
            lstProtocol.BackColor = backColor;
            tbHost.BackColor = backColor;
            tbPort.BackColor = backColor;
            tbUserName.BackColor = backColor;
            tbPassword.BackColor = backColor;
            tbTopic.BackColor = backColor;
            tbParsingText.BackColor = backColor;

            lstProtocol.ForeColor = foreColor;
            tbHost.ForeColor = foreColor;
            tbPort.ForeColor = foreColor;
            tbUserName.ForeColor = foreColor;
            tbPassword.ForeColor = foreColor;
            tbTopic.ForeColor = foreColor;
            tbParsingText.ForeColor = foreColor;

            lblProtocol.ForeColor = lblForeColor;
            lblHost.ForeColor = lblForeColor;
            lblPort.ForeColor = lblForeColor;
            lblUserName.ForeColor = lblForeColor;
            lblPassword.ForeColor = lblForeColor;
            lblTopic.ForeColor = lblForeColor;
            grbConnection.ForeColor = lblForeColor;
            grbParsingText.ForeColor = lblForeColor;

            btnConnect.Text = connected ? "Disconnect" : "Connect";                           // 버튼 텍스트 변경
            btnConnect.BackColor = connected ? Color.IndianRed : Color.Turquoise;             // 버튼 색상 변경
            btnConnect.ForeColor = connected ? Color.White : SystemColors.WindowText;         // 버튼 텍스트 색상 변경

            tbHost.ReadOnly = connected;
            tbPort.ReadOnly = connected;
            tbUserName.ReadOnly = connected;
            tbPassword.ReadOnly = connected;
            tbTopic.ReadOnly = connected;
            tbParsingText.ReadOnly = connected;

            this.Update(); // UI 변경사항 즉시 반영
        }

        // 입력란의 에러 상태에 따라 배경색을 변경, 에러 여부 반환
        private bool InputError_Highlight(TextBox tb, bool isError)
        {
            RunOnUIThread(tb, () => tb.BackColor = isError ? Color.Red : defaultInputBackColor); // 에러면 빨간색, 아니면 기본색으로 배경색 변경(스레드 안전)
            return isError;                                                                      // 에러 여부 반환
        }

        // 하단 안내 메시지 텍스트와 색상을 지정, 패널 오른쪽 끝으로 이동
        private void BottomMSG(string msg, Color color)
        {
            RunOnUIThread(lblMSG, () =>     // UI 스레드에서 안전하게 실행
            {
                lblMSG.Text = msg;          // 하단 라벨에 안내 메시지 텍스트 설정
                lblMSG.ForeColor = color;   // 하단 라벨의 글자색을 지정한 색상으로 변경
                lblMSG.Left = pnlMSG.Width; // 하단 라벨의 시작 위치를 패널 오른쪽 끝으로 이동(애니메이션 시작점)
            });
        }

        // 하단 안내 메시지(lblMSG) 패널(pnlMSG) 내 좌우로 반복 이동
        private void BottomMSG_Scroll()
        {
            RunOnUIThread(lblMSG, () =>                              // UI 스레드에서 안전하게 실행
            {
                if (pnlMSG.Width >= lblMSG.Width)                    // 하단 메시지 길이가 패널보다 짧으면
                {
                    lblMSG.Left = (pnlMSG.Width - lblMSG.Width) / 2; // 라벨을 패널 중앙에 정렬
                    return;                                          // 이동 중지
                }
                lblMSG.Left += ScrollSpeed * ScrollDirection;        // 현재 방향으로 라벨 위치 이동

                if (lblMSG.Left <= pnlMSG.Width - lblMSG.Width)      // 왼쪽 경계(패널 오른쪽 끝) 도달 시
                {
                    lblMSG.Left = pnlMSG.Width - lblMSG.Width;       // 라벨을 왼쪽 경계에 고정
                    ScrollDirection = 1;                             // 이동 방향을 오른쪽(1)으로 반전
                }
                else if (lblMSG.Left >= 0)                           // 오른쪽 경계(패널 왼쪽 끝) 도달 시
                {
                    lblMSG.Left = 0;                                 // 라벨을 오른쪽 경계에 고정
                    ScrollDirection = -1;                            // 이동 방향을 왼쪽(-1)으로 반전
                }
            });
        }

        // 각 컨트롤 라인 제한 현황 갱신 및 라벨 위치 우측 정렬
        private void LineLabels_Update()
        {
            // 최소 100ms 간격으로만 UI 업데이트 실행
            if ((DateTime.Now - _lastUIUpdate).TotalMilliseconds < 100)                              // 마지막 업데이트 후 100ms 경과 확인
            {
                _uiUpdatePending = true;                                                             // 업데이트 대기 플래그 설정
                return;                                                                              // 함수 종료
            }

            if (this.InvokeRequired)                                                                 // UI 스레드가 아니면
            {
                this.Invoke(new Action(LineLabels_Update));                                          // UI 스레드에서 재호출
                return;                                                                              // 함수 종료
            }

            _lastUIUpdate = DateTime.Now;                                                            // 마지막 업데이트 시간 기록
            _uiUpdatePending = false;                                                                // 업데이트 대기 플래그 해제

            int systemMsgLines = Math.Max(0, (rtbSystemMSG?.Lines.Length ?? 0) - 1);                 // 시스템 메시지 라인 수 계산(음수 방지)
            int mqttMonitorLins = Math.Max(0, (rtbMQTTmonitor?.Lines.Length ?? 0) - 1);              // MQTT 모니터 라인 수 계산(음수 방지)
            int parsingDataLines = Math.Max(0, (rtbParsingData?.Lines.Length ?? 0));                 // 파싱 데이터 라인 수 계산(음수 방지)

            lblSystemMSG.Text = $"화면출력 제한 : {systemMsgLines:N0} / {systemMsgLimit:N0} lines";  // 시스템 메시지 제한 라벨 텍스트 설정
            lblMQTTmonitor.Text = $"화면출력 제한 : {mqttMonitorLins:N0} / {mqttLimit:N0} lines";    // MQTT 모니터 제한 라벨 텍스트 설정
            lblParsingData.Text = $"화면출력 제한 : {parsingDataLines:N0} / {parsedLimit:N0} lines"; // 파싱 데이터 제한 라벨 텍스트 설정

            LineLabels_Location(lblSystemMSG, grbSystemMSG);                                         // 시스템 메시지 라벨 위치 조정
            LineLabels_Location(lblMQTTmonitor, grbMQTTmonitor);                                     // MQTT 모니터 라벨 위치 조정
            LineLabels_Location(lblParsingData, grbParsingData);                                     // 파싱 데이터 라벨 위치 조정

            this.Update();                                                                           // UI 변경사항 즉시 반영
        }

        // 라벨 항상 우측 정렬, 텍스트 길이에 따라 동적 너비 조정
        private void LineLabels_Location(System.Windows.Forms.Label label, System.Windows.Forms.Control groupBox)
        {
            RunOnUIThread(label, () =>                          // UI 스레드에서 안전하게 실행
            {
                label.AutoSize = false;                         // 라벨 크기를 자동이 아닌 수동으로 지정
                label.TextAlign = ContentAlignment.MiddleRight; // 라벨 텍스트를 오른쪽에 정렬

                using (var g = label.CreateGraphics())          // 라벨의 그래픽 객체 생성(텍스트 크기 측정용)
                {
                    var textSize = g.MeasureString(label.Text, label.Font); // 현재 텍스트와 폰트로 텍스트 크기 측정
                    int padding = 10;                           // 그룹박스 오른쪽 여백(픽셀 단위)
                    int minWidth = 110;                         // 라벨 최소 너비(픽셀 단위)
                    int width = Math.Max((int)Math.Ceiling(textSize.Width), minWidth); // 텍스트 크기와 최소값 중 큰 값 사용
                    width = Math.Min(width, groupBox.ClientSize.Width - padding); // 그룹박스 너비를 넘지 않도록 제한

                    label.Width = width;                        // 계산된 너비로 라벨 크기 지정
                    label.Left = groupBox.ClientSize.Width - label.Width - padding; // 그룹박스 우측에 맞춰 라벨 위치 지정
                    label.Top = 0;                              // 라벨의 Y 위치를 항상 0(상단)으로 설정
                }
            });
        }

        // 폼 리사이즈 시 라벨 우측에 정렬, 최소화 시 MinimumSize로 변경
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);                                    // 기본 폼 리사이즈 동작 실행
            LineLabels_Location(lblSystemMSG, grbSystemMSG);     // 시스템 메시지 라벨 우측 정렬
            LineLabels_Location(lblMQTTmonitor, grbMQTTmonitor); // MQTT 모니터 라벨 우측 정렬
            LineLabels_Location(lblParsingData, grbParsingData); // 파싱 데이터 라벨 우측 정렬

            if (WindowState == FormWindowState.Minimized)        // 폼이 최소화 상태면
            {
                WindowState = FormWindowState.Normal;            // 폼 상태를 일반으로 복원
                Size = MinimumSize;                              // 폼 크기를 MinimumSize로 설정

                Screen screen = Screen.FromPoint(Cursor.Position);       // 현재 스크린의 작업 영역(작업 표시줄 제외) 가져오기
                Rectangle workingArea = screen.WorkingArea;
                this.Location = new Point(workingArea.Left, workingArea.Bottom - this.Height);
            }
        }

        // 양쪽 SplitterDistance 동기화
        private void SyncSplitter(object sender, SplitterEventArgs e)
        {
            if (_syncingPanels) return;                                      // 동기화 중 재진입 방지(무한루프 방지)
            _syncingPanels = true;                                           // 동기화 시작 플래그 설정

            try
            {
                if ((spContainer_R.Height - spContainer_R.SplitterDistance - spContainer_R.SplitterWidth) >= spContainer_R.Panel2MinSize &&
                        (spContainer_L.Height - spContainer_L.SplitterDistance - spContainer_L.SplitterWidth) >= spContainer_L.Panel2MinSize)
                {
                    if (sender == spContainer_L)                             // 왼쪽 SplitContainer에서 이벤트 발생 시
                    {
                        spContainer_R.SplitterDistance = spContainer_L.SplitterDistance;     // 오른쪽 SplitContainer의 SplitterDistance를 왼쪽과 동일하게 설정

                        if (spContainer_L.Panel2.Height < spContainer_R.Panel2.Height)       // 왼쪽 Panel2 높이가 오른쪽보다 작으면
                        {
                            spContainer_L.SplitterDistance = spContainer_R.SplitterDistance; // 왼쪽 SplitterDistance를 오른쪽과 동일하게 재설정
                        }
                    }
                    else if (sender == spContainer_R)                        // 오른쪽 SplitContainer에서 이벤트 발생 시
                    {
                        spContainer_L.SplitterDistance = spContainer_R.SplitterDistance;     // 왼쪽 SplitContainer의 SplitterDistance를 오른쪽과 동일하게 설정

                        if (spContainer_R.Panel2.Height < spContainer_L.Panel2.Height)       // 오른쪽 Panel2 높이가 왼쪽보다 작으면
                        {
                            spContainer_R.SplitterDistance = spContainer_L.SplitterDistance; // 오른쪽 SplitterDistance를 왼쪽과 동일하게 재설정
                        }
                    }
                    else                                                     // sender가 두 SplitContainer 모두 아닌 경우
                    {
                        spContainer_L.SplitterDistance = Math.Max(
                        spContainer_L.SplitterDistance, spContainer_R.SplitterDistance);     // 두 SplitterDistance 중 큰 값으로 왼쪽 설정
                        spContainer_R.SplitterDistance = Math.Max(
                            spContainer_L.SplitterDistance, spContainer_R.SplitterDistance); // 두 SplitterDistance 중 큰 값으로 오른쪽 설정
                    }
                }
            }
            finally
            {
                _syncingPanels = false;                                      // 동기화 종료 플래그 해제
                //lblParsingData.Text = (spContainer_R.Height - spContainer_R.SplitterDistance - spContainer_R.SplitterWidth).ToString() + " // " + (spContainer_L.Height - spContainer_L.SplitterDistance - spContainer_L.SplitterWidth).ToString(); // 디버깅용: 파싱 데이터 라벨에 왼쪽 SplitterDistance 표시
            }
        }

        // tbParsingText의 Enter 입력 허용(Form의 AcceptButton 차단)
        private void tbParsingText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)       // 입력된 키가 Enter인지 확인
            {
                e.Handled = true;              // AcceptButton 동작(연결 버튼 클릭) 차단
                e.SuppressKeyPress = false;    // Enter 키 입력은 그대로 허용(줄바꿈 가능)
            }
        }

        // MQTT 브로커 연결/해제 전환, 연결 상태에 따라 리소스 생성 또는 정리
        private async void btnConnectClick(object sender, EventArgs e)
        {
            try
            {
                if (!isConnected) // 연결되지 않은 상태
                {
                    // 이전 재연결 취소 토큰 정리
                    if (reconnectCts != null)
                    {
                        reconnectCts.Cancel();                               // 재연결 취소 요청
                        reconnectCts.Dispose();                              // 리소스 해제
                        reconnectCts = null;                                 // 참조 해제
                    }

                    ServerList_Add_Update(
                        tbHost.Text.Trim(),                                  // 호스트 입력값 저장
                        int.TryParse(tbPort.Text.Trim(), out var port) ? port : 1883, // 포트 입력값 저장(실패 시 1883)
                        tbUserName.Text.Trim(),                              // 사용자명 입력값 저장
                        tbPassword.Text.Trim(),                              // 비밀번호 입력값 저장
                        tbTopic.Text.Trim()                                  // 토픽 입력값 저장
                    );

                    ConnectionUI_Update(true);                               // UI 연결 상태로 갱신

                    if (fileWriter == null)
                        btnSaveFileClick(sender, EventArgs.Empty);           // 파일 저장 시작

                    // 헤더 기록 대기
                    using var headerWaitCts = new CancellationTokenSource(30000); // 30초 타임아웃
                    int headerWait = 0;                                      // 대기 카운터
                    int dly = 1000;                                          // 대기 간격(ms)
                    while (!isHeaderWritten && !headerWaitCts.Token.IsCancellationRequested) // 헤더 기록 완료 또는 타임아웃까지 대기
                    {
                        await Task.Delay(dly, headerWaitCts.Token);          // 지정 간격 대기
                        headerWait++;                                        // 카운터 증가
                        if (headerWait % 10 == 0)
                            SystemMessage_Add($"헤더 기록 대기 중... {headerWait * dly}ms 경과, isHeaderWritten={isHeaderWritten}", Color.OrangeRed, true, false); // 10초마다 대기 메시지 출력
                    }
                    if (!isHeaderWritten)                                    // 헤더 기록 실패
                    {
                        SystemMessage_Add("파일 헤더 기록 실패: 데이터 기록을 시작하지 않음", Color.OrangeRed, true, false); // 실패 메시지 출력
                        ConnectionUI_Update(false);                          // UI 연결 해제 상태로 복원
                        return;                                              // 함수 종료
                    }

                    // UI 갱신 타이머 설정
                    uiUpdateTimer?.Stop();                                   // 기존 타이머 중지
                    uiUpdateTimer?.Dispose();                                // 리소스 해제
                    uiUpdateTimer = new System.Windows.Forms.Timer
                    {
                        Interval = 1000                                      // 1초 갱신 간격
                    };

                    int uiUpdateRunning = 0;                                 // 중복 실행 방지 플래그
                    uiUpdateTimer.Tick += (s, ee) =>
                    {
                        if (Interlocked.CompareExchange(ref uiUpdateRunning, 1, 0) != 0)
                            return;                                          // 이미 실행 중이면 중단

                        try
                        {
                            DiagnosticsLog("UI Timer Tick");                 // 진단 로그 기록

                            RunOnUIThread(this, () =>
                            {
                                MonitorBuffer_Flush();                       // MQTT 모니터 버퍼 출력
                                ParsedRows_Flush();                          // 파싱 데이터 버퍼 출력
                                this.Refresh();                              // 폼 새로고침
                            });
                        }
                        finally
                        {
                            Interlocked.Exchange(ref uiUpdateRunning, 0);    // 플래그 해제
                        }
                    };

                    uiUpdateTimer.Start();                                   // UI 갱신 타이머 시작

                    ResetTime = DateTime.Now;                                // 메모리 체크 기준 시각 갱신

                    StartParsingWorker();                                    // 파싱 워커 시작

                    LogCleaner();                                            // 로그 정리 워커 시작

                    isManualDisconnect = false;                              // 수동 해제 플래그 해제

                    if (!BuildMqttOptions(out var options, out string topic)) // MQTT 연결 옵션 생성
                    {
                        SystemMessage_Add("MQTT 연결 옵션 생성 실패: 입력값 확인 필요", Color.OrangeRed, true, false); // 실패 메시지 출력
                        ConnectionUI_Update(false);                          // UI 연결 해제 상태로 복원
                        return;                                              // 함수 종료
                    }

                    lastMqttOptions = options;                               // 연결 옵션 저장
                    lastMqttTopic = topic;                                   // 구독 토픽 저장

                    var factory = new MqttFactory();                         // MQTT 팩토리 생성
                    mqttClient = factory.CreateMqttClient();                 // 클라이언트 인스턴스 생성
                    MQTT_EventsHandler_Register();                           // 이벤트 핸들러 등록

                    try
                    {
                        await mqttClient.ConnectAsync(options);              // MQTT 브로커 연결 시도
                        if (mqttClient.IsConnected)                          // 연결 성공
                        {
                            SystemMessage_Add("MQTT 연결 성공", Color.RoyalBlue, true, false); // 성공 메시지 출력
                            await mqttClient.SubscribeAsync(topic);          // 토픽 구독
                            SystemMessage_Add("구독 요청: " + topic, Color.RoyalBlue, true, false); // 구독 메시지 출력
                            SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED | ES_SYSTEM_REQUIRED); // 절전 방지 설정

                            MemoryMonitor_Start();                           // 메모리 모니터링 시작
                        }
                        else                                                 // 연결 실패
                        {
                            SystemMessage_Add("MQTT 연결 실패: 브로커 응답 없음", Color.OrangeRed, true, false); // 실패 메시지 출력
                            ConnectionUI_Update(false);                      // UI 연결 해제 상태로 복원
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        SystemMessage_Add("MQTT 연결 시도 취소됨", Color.Black, true, false); // 취소 메시지 출력
                        ConnectionUI_Update(false);                          // UI 연결 해제 상태로 복원
                    }
                    catch (Exception ex)
                    {
                        SystemMessage_Add($"btnConnectClick(object sender, EventArgs e) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 메시지 출력
                    }
                }
                else                                                         // 이미 연결된 상태(해제 요청)
                {
                    isManualDisconnect = true;                               // 수동 해제 플래그 설정

                    _parsingWorkerCts.Cancel();                              // 파싱 워커 취소
                    reconnectCts?.Cancel();                                  // 재연결 취소
                    reconnectCts = null;                                     // 참조 해제

                    MonitorBuffer_Flush();                                   // MQTT 모니터 버퍼 출력
                    ParsedRows_Flush();                                      // 파싱 데이터 버퍼 출력

                    uiUpdateTimer?.Stop();                                   // UI 갱신 타이머 중지
                    uiUpdateTimer?.Dispose();                                // 리소스 해제
                    uiUpdateTimer = null;                                    // 참조 해제

                    MemoryMonitor_Stop();                                    // 메모리 모니터링 중지

                    systemMsgCts?.Cancel();                                  // 시스템 메시지 정리 취소
                    mqttMonitorCts?.Cancel();                                // MQTT 모니터 정리 취소
                    parsingDataCts?.Cancel();                                // 파싱 데이터 정리 취소

                    if (mqttClient != null)
                    {
                        try
                        {
                            if (mqttClient.IsConnected)
                                await mqttClient.DisconnectAsync();          // MQTT 연결 해제
                            mqttClient.Dispose();                            // 리소스 해제
                        }
                        catch (Exception ex)
                        {
                            SystemMessage_Add($"btnConnectClick(object sender, EventArgs e) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 메시지 출력
                        }
                        mqttClient = null;                                   // 참조 해제
                    }

                    if (fileWriter != null)
                        btnSaveFileClick(sender, EventArgs.Empty);           // 파일 저장 중지

                    ConnectionUI_Update(false);                              // UI 연결 해제 상태로 복원
                    SetThreadExecutionState(ES_CONTINUOUS);                  // 절전 방지 해제
                }
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"btnConnectClick(object sender, EventArgs e) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 메시지 출력
                ConnectionUI_Update(false);                                  // UI 연결 해제 상태로 복원
            }
        }

        // 로그성 데이터(RichTextBox, DataGridView) 전체 초기화 (사용자 확인)
        private void btnClearClick(object sender, EventArgs e)
        {
            RunOnUIThread(this, () =>
            {
                var result = MessageBox.Show(
                    "화면에 표시된 데이터를 모두 지우시겠습니까?",
                    "확인",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ControlClear();
                }
            });
        }

        // 컨트롤 및 내부 리스트 초기화
        private void ControlClear()
        {
            // 내부 리스트도 함께 초기화
            //lock (SystemMsgMessages) SystemMsgMessages.Clear();       // 시스템 메시지 리스트 초기화
            lock (LogMessages) LogMessages.Clear();                   // 파싱 데이터 메시지 리스트 초기화
            lock (mqttColorMessages) mqttColorMessages.Clear();       // MQTT 모니터 색상 메시지 버퍼 초기화
            lock (monitorMsgLock) monitorMsgBuffer.Clear();           // MQTT 모니터 버퍼 초기화
            lock (pendingParsedRows) pendingParsedRows.Clear();       // 파싱 데이터 버퍼 초기화
            lock (pendingParsedHeaders) pendingParsedHeaders.Clear(); // 파싱 헤더 버퍼 초기화
            parsedRows.Clear();                                       // 파싱 데이터 행 목록 초기화
            dynamicHeaders.Clear();                                   // 동적 헤더 목록 초기화

            //rtbSystemMSG.Clear();                                     // 시스템 메시지 텍스트박스 내용 삭제
            rtbMQTTmonitor.Clear();                                   // MQTT 모니터 텍스트박스 내용 삭제
            rtbParsingData.Clear();                                   // 파싱 데이터 텍스트박스 내용 삭제

            dgvParsingData.Rows.Clear();                              // 파싱 데이터 그리드뷰 행 전체 삭제
            dgvParsingData.RowCount = 0;                              // 행 수를 0으로 설정
            dgvParsingData.Columns.Clear();                           // 컬럼 초기화

            LineLimit(rtbMQTTmonitor, mqttLimit);                     // 라인 제한 적용
            LineLimit(rtbParsingData, parsedLimit);                   // 라인 제한 적용

            DiagnosticsLog("OnClearClick 후 상태");                   // 로그 진단 메시지 출력
            LineLabels_Update();                                      // 라벨 갱신

            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        // 파싱 데이터 파일 저장 시작/중지, UI 및 파일 리소스 상태 동기화
        private void btnSaveFileClick(object sender, EventArgs e)
        {
            try
            {
                var parsingRules = tbParsingText.Lines.Where(r => !string.IsNullOrWhiteSpace(r)).ToArray(); // 파싱 규칙 입력란에서 공백 제외한 줄 추출
                var rules = ExtractRules(parsingRules);                // 실제 파싱 규칙만 추출(만족도 등 제외)
                if (rules == null || rules.Length == 0)                // 파싱 규칙이 없으면
                {
                    SystemMessage_Add("파싱 규칙이 없습니다. 파싱 규칙을 먼저 입력하세요.", Color.OrangeRed, true, false); // 시스템 메시지 출력
                    MessageBox.Show("파싱 규칙을 먼저 입력하세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Warning); // 경고 메시지 박스 표시
                    return;                                            // 함수 종료
                }
                lastHeaderFields = rules.ToList();                     // 마지막 헤더 필드 목록 갱신

                FileHeader_Check();                                    // 파일 헤더 없으면 추가(헤더 보정)

                if (fileWriter == null)                                // 파일 저장 비활성화 상태(저장 시작)
                {
                    try
                    {
                        if (!argsParsedfilePath)
                        {
                            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"; // 파일 확장자 필터 지정
                            saveFileDialog.Title = "파싱 데이터 실시간 저장";  // 파일 저장 다이얼로그 타이틀 지정
                            saveFileDialog.InitialDirectory = exePath;         // 파일 저장 기본 폴더 지정
                            saveFileDialog.FileName = "ParsingData.txt";       // 기본 파일명 지정
                            saveFileDialog.FileName = exePath + "\\ParsingData.txt"; // 기본 파일 경로 지정
                            saveFileDialog.OverwritePrompt = false;            // 덮어쓰기 경고 비활성화

                            //if (saveFileDialog.ShowDialog() == DialogResult.OK) // 파일 저장 다이얼로그에서 파일 선택 완료 시
                            //{
                            ParsingDataFilePath = saveFileDialog.FileName;     // 선택된 파일 경로 저장
                            fileWriter?.Dispose();                             // 기존 Writer 있으면 해제
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        SystemMessage_Add($"btnSaveFileClick(object sender, EventArgs e) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
                        return;                                        // 함수 종료
                    }

                    if (!string.IsNullOrEmpty(ParsingDataFilePath))    // 파일 경로가 null 또는 빈 문자열이 아니면
                    {
                        fileWriter = new StreamWriter(ParsingDataFilePath, append: true, System.Text.Encoding.UTF8); // 새 Writer 생성(추가 모드)
                    }
                    else
                    {
                        SystemMessage_Add($"파일 경로가 지정되지 않았습니다. 파일 저장을 건너뜁니다. filePath: \"{ParsingDataFilePath}\"", Color.Red, true, false); // 경고 메시지 출력
                        return; // 함수 종료
                    }

                    if (lastHeaderFields != null && lastHeaderFields.Count > 0) // 헤더 필드가 있으면
                    {
                        string headerLine = string.Join("\t", lastHeaderFields) + "\r\n"; // 헤더 라인 생성
                        fileWriter.Write(headerLine);                  // 헤더 파일에 기록
                        fileWriter.Flush();                            // 버퍼 즉시 반영
                        isHeaderWritten = true;                        // 헤더 기록 플래그 설정
                    }

                    btnSaveFile.Text = "Stop Saving";                  // 버튼 텍스트 변경(저장 중)
                    lblMSG.DoubleClick += lblMSG_DoubleClick;          // 하단 더블클릭 이벤트 등록
                    BottomMSG("파싱 데이터 실시간 저장: " + ParsingDataFilePath, Color.Blue); // 하단 안내 메시지 및 색상 변경
                    btnSaveFile.BackColor = Color.IndianRed;           // 버튼 색상 변경(저장 중 표시)
                    btnSaveFile.ForeColor = Color.White;               // 버튼 텍스트 색상 변경
                    FileWriterStart();                                 // 파일 저장 워커 비동기 시작
                    //}
                }
                else                                                   // 파일 저장 활성화 상태(저장 중지)
                {
                    fileWriteCts?.Cancel();                            // 파일 저장 워커 취소 요청
                    fileWriteCts?.Token.WaitHandle.WaitOne(500);       // 워커 종료 대기(최대 500ms)
                    fileWriter?.Flush();                               // 파일 버퍼 즉시 반영
                    fileWriter?.Close();                               // Writer 닫기
                    fileWriter?.Dispose();                             // Writer 리소스 해제
                    fileWriter = null;                                 // Writer 참조 해제
                    ParsingDataFilePath = null;                        // 파일 경로 참조 해제
                    lblMSG.DoubleClick -= lblMSG_DoubleClick;          // 하단 더블클릭 이벤트 해제
                    btnSaveFile.BackColor = Color.Turquoise;           // 버튼 색상 복원(저장 중지 표시)
                    btnSaveFile.ForeColor = SystemColors.WindowText;   // 버튼 텍스트 색상 복원
                    btnSaveFile.Text = "Save to File";                 // 버튼 텍스트 변경(저장 시작)
                    BottomMSG("파싱 데이터 저장 중지됨. 파싱 데이터 저장 기능은 [Save to File] 버튼을 눌러 파일저장을 시작한 시점부터 파싱된 데이터가 저장됩니다.", Color.Red); // 하단 안내 메시지 및 색상 복원
                }
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"btnSaveFileClick(object sender, EventArgs e) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
            }
        }

        // 닫기 버튼 클릭 (MQTT 연결 해제 또는 폼 종료)
        private void btnCloseClick(object sender, EventArgs e)
        {
            if (isConnected)                                      // 현재 MQTT 연결 상태인 경우
            {
                var result = MessageBox.Show(
                    "서버와의 연결을 해제하시겠습니까?",          // 접속해제 확인 메시지
                    "연결 해제",                                  // 타이틀
                    MessageBoxButtons.YesNo,                      // 예/아니오 버튼
                    MessageBoxIcon.Question);                     // 질문 아이콘

                if (result == DialogResult.No)                    // 사용자가 '아니오'를 선택한 경우
                {
                    return;                                       // 함수 종료
                }

                btnConnectClick(sender, e);                       // 연결 해제(Disconnect) 처리 호출
                SetThreadExecutionState(ES_CONTINUOUS);           // 시스템 절전 방지 상태 복원
            }
            else                                                  // 이미 해제된 상태인 경우
            {
                SetThreadExecutionState(ES_CONTINUOUS);           // 시스템 절전 방지 상태 복원
                Close();                                          // 폼 종료 요청
            }
        }

        // 도움말 버튼 클릭 (도움말 표시)
        private void btnHelp_Click(object sender, EventArgs e)
        {
            help();
        }

        // 도움말 표시 함수
        private void help()
        {
            string exeFullPath = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName ?? string.Empty; // 실행 파일 경로 추출

            // 이미 열린 도움말 폼이 있으면 포커스만 이동
            if (helpFormInstance != null && !helpFormInstance.IsDisposed)
            {
                helpFormInstance.Activate();
                return;
            }

            // 새 도움말 폼 생성 및 인스턴스 저장
            helpFormInstance = new Help(exeFullPath, this);
            helpFormInstance.FormClosed += (s, e) => helpFormInstance = null; // 폼 닫힐 때 인스턴스 해제
            helpFormInstance.Show();
        }

        // 폼 종료 처리(리소스 해제, 상태 저장, 예외 처리 보완)
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var result = MessageBox.Show(
                "프로그램을 종료하시겠습니까?",
                "종료 확인",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            SetThreadExecutionState(ES_CONTINUOUS);                    // 시스템 절전 방지 상태 복원

            try
            {
                _parsingWorkerCts.Cancel();                            // 파싱 워커 취소 토큰 설정
                _parsingWorkerTask?.Wait(1000);                        // 파싱 워커 종료 대기(최대 1초)

                fileWriter?.Flush();                                   // 파일 버퍼 강제 저장
                fileWriter?.Dispose();                                 // 파일 Writer 리소스 해제
                fileWriter = null;                                     // 파일 Writer 참조 해제

                fileWriteCts?.Cancel();                                // 파일 저장 워커 취소 토큰 설정
                fileWriteCts = null;                                   // 파일 저장 취소 토큰 참조 해제

                _uiUpdateThrottleTimer?.Dispose();                     // UI 업데이트 스로틀 타이머 해제
                marqueeTimer?.Dispose();                               // 하단 애니메이션 타이머 해제
                marqueeTimer = null;                                   // 하단 애니메이션 타이머 참조 해제
                memoryTimer?.Dispose();                                // 메모리 모니터링 타이머 해제
                memoryTimer = null;                                    // 메모리 모니터링 타이머 참조 해제
                uiUpdateTimer?.Stop();                                 // UI 갱신 타이머 중지
                uiUpdateTimer?.Dispose();                              // UI 갱신 타이머 리소스 해제
                uiUpdateTimer = null;                                  // UI 갱신 타이머 참조 해제

                systemMsgCts?.Cancel();                                // 시스템 메시지 정리 취소 토큰 설정
                systemMsgCts = null;                                   // 시스템 메시지 정리 취소 토큰 참조 해제
                mqttMonitorCts?.Cancel();                              // MQTT 모니터 정리 취소 토큰 설정
                mqttMonitorCts = null;                                 // MQTT 모니터 정리 취소 토큰 참조 해제
                parsingDataCts?.Cancel();                              // 파싱 데이터 정리 취소 토큰 설정
                parsingDataCts = null;                                 // 파싱 데이터 정리 취소 토큰 참조 해제

                // MQTT 클라이언트 및 이벤트 핸들러 해제
                if (mqttClient != null)                                // MQTT 클라이언트 존재 시
                {
                    try
                    {
                        if (mqttClient.IsConnected)                    // MQTT 브로커 연결 상태 확인
                        {
                            var disconnectTask = mqttClient.DisconnectAsync(); // 연결 해제 요청
                            // 1초 이내에 완료 안되면 강제 종료
                            var completed = Task.WaitAll(new[] { disconnectTask }, 1000); // 연결 해제 완료 대기(최대 1초)
                            if (!completed)                            // 연결 해제 타임아웃 시
                            {
                                SystemMessage_Add("MQTT 연결 해제 타임아웃, 강제 종료합니다.", Color.OrangeRed, true, false); // 강제 종료 메시지 출력
                            }
                        }
                    }
                    catch (Exception ex)                               // 예외 발생 시
                    {
                        SystemMessage_Add($"OnFormClosing(FormClosingEventArgs e) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 정보 시스템 메시지로 출력
                    }

                    mqttClient.Dispose();                              // MQTT 클라이언트 리소스 해제
                    mqttClient = null;                                 // MQTT 클라이언트 참조 해제
                }

                lblMSG.DoubleClick -= lblMSG_DoubleClick;              // 하단 라벨 더블클릭 이벤트 핸들러 제거

                _parsingSemaphore?.Dispose();                          // 파싱 세마포어 리소스 해제

                GC.WaitForPendingFinalizers();                         // 보류 중인 파이널라이저 대기
                GC.Collect();                                          // 가비지 컬렉션 강제 실행
            }
            catch (Exception ex)                                       // 예외 발생 시
            {
                SystemMessage_Add($"OnFormClosing(FormClosingEventArgs e) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 정보 시스템 메시지로 출력
            }

            base.OnFormClosing(e);                                     // 기본 폼 종료 로직 호출
        }

        // 비밀번호 입력란 마스킹 문자 표시/해제 토글
        private void lblPassword_DoubleClick(object sender, EventArgs e)
        {
            tbPassword.PasswordChar = tbPassword.PasswordChar == '·' ? '\0' : '·'; // 현재 마스킹 문자면 해제('\0'), 아니면 마스킹('·')으로 변경
        }

        // 하단 라벨 더블클릭 시, 파일 또는 탐색기 열기
        private void lblMSG_DoubleClick(object? sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ParsingDataFilePath))               // 파일 경로가 null이 아니고 비어 있지 않은지 확인
                {
                    try
                    {
                        if (System.IO.File.Exists(ParsingDataFilePath))       // 파일이 실제로 존재하는지 확인
                        {
                            var psi = new System.Diagnostics.ProcessStartInfo // 탐색기 프로세스 정보 객체 생성
                            {
                                FileName = "explorer.exe",                    // 실행할 파일명 지정(탐색기)
                                Arguments = $"/select,\"{ParsingDataFilePath}\"",        // 파일 선택 인자 전달
                                UseShellExecute = true                        // 셸 실행 방식 지정
                            };
                            System.Diagnostics.Process.Start(psi);            // 탐색기 프로세스 실행(파일 선택)
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(ParsingDataFilePath))
                            {
                                string? folder = System.IO.Path.GetDirectoryName(ParsingDataFilePath); // 파일 경로에서 폴더 경로 추출
                                if (!string.IsNullOrEmpty(folder) && System.IO.Directory.Exists(folder)) // 폴더 경로가 유효하고 폴더가 존재하는지 확인
                                {
                                    var psi = new System.Diagnostics.ProcessStartInfo // 탐색기 프로세스 정보 객체 생성
                                    {
                                        FileName = "explorer.exe",                // 실행할 파일명 지정(탐색기)
                                        Arguments = $"\"{folder}\"",              // 폴더 열기 인자 전달
                                        UseShellExecute = true                    // 셸 실행 방식 지정
                                    };
                                    System.Diagnostics.Process.Start(psi);        // 탐색기 프로세스 실행(폴더 열기)
                                }
                                else
                                {
                                    MessageBox.Show("폴더를 찾을 수 없습니다.",   // 폴더가 없으면 오류 메시지 박스 표시
                                        "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)                                          // 탐색기 실행 중 예외 발생 시
                    {
                        SystemMessage_Add($"lblMSG_DoubleClick(object? sender, EventArgs e) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
                    }
                }
                else
                {
                    MessageBox.Show("저장된 파일이 없습니다.",                    // 파일 경로가 없으면 안내 메시지 박스 표시
                        "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)                                                  // 더블클릭 이벤트 처리 중 예외 발생 시
            {
                SystemMessage_Add($"lblMSG_DoubleClick(object? sender, EventArgs e) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
            }
        }

        #endregion



        #region 2. 로그/메시지

        // 시스템 메시지 추가
        private void SystemMessage_Add(string text, Color color, bool blTimeStamp, bool blAddMSG)
        {
            string msg;
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); // 현재 시각을 타임스탬프로 생성
            if (!blAddMSG)
            {
                msg = $"\r\n{timestamp}\t{text}";                   // 타임스탬프와 메시지를 결합하여 한 줄 생성
            }
            else
            {
                msg = text;
            }

            RunOnUIThread(rtbSystemMSG, () =>                       // UI 스레드에서 안전하게 실행
            {
                List<string> copy;                                  // 메시지 복사본 리스트 선언
                lock (SystemMsgMessages)                            // 메시지 리스트 동시 접근 방지
                {
                    copy = new List<string>(SystemMsgMessages);     // 메시지 리스트 복사
                }
                //rtbSystemMSG.Text = string.Concat(copy);            // 전체 메시지를 RichTextBox에 반영
                rtbSystemMSG.SelectionStart = rtbSystemMSG.TextLength;
                rtbSystemMSG.SelectionLength = 0;
                rtbSystemMSG.SelectionColor = color;
                if (blTimeStamp)
                {
                    if (!blAddMSG)
                    {
                        rtbSystemMSG.AppendText($"\r\n{timestamp} \t ");
                    }
                    else
                    {
                        rtbSystemMSG.AppendText($"{timestamp} \t ");
                    }
                }
                rtbSystemMSG.AppendText(text);
                rtbSystemMSG.SelectionColor = rtbSystemMSG.ForeColor;

                lock (SystemMsgMessages)                            // 시스템 메시지 리스트 동시 접근 방지
                {
                    SystemMsgMessages.Clear();                      // 기존 메시지 리스트 초기화
                    var lines = rtbSystemMSG.Lines;                 // RichTextBox의 모든 라인 배열 추출
                    int startIdx = Math.Max(0, lines.Length - systemMsgLimit - 1); // 최근 메시지부터 제한량만큼 시작 인덱스 계산
                    for (int i = startIdx; i < lines.Length; i++)   // 최근 메시지부터 제한량만큼 반복
                        SystemMsgMessages.Add(lines[i]);
                }
                LineLimit(rtbSystemMSG, systemMsgLimit);            // 최대 라인 수 초과 시 앞부분 삭제 및 커서 이동
            });
        }

        // RichTextBox에 텍스트를 추가, 제한 내 최신 메시지 유지
        private void ParsedText_Add(RichTextBox tb, string text, int maxLines)
        {
            if (tb == null || tb.IsDisposed) return;                // 컨트롤이 null이거나 해제된 경우 함수 종료

            RunOnUIThread(tb, () =>                                 // UI 스레드에서 안전하게 실행
            {
                if (tb == rtbParsingData)                           // 파싱 데이터 RichTextBox인 경우
                {
                    lock (LogMessages) LogMessages.Add(text);       // 메시지 리스트에 텍스트 추가(동기화)
                    List<string> copy;                              // 복사본 리스트 선언
                    lock (LogMessages) copy = new List<string>(LogMessages); // 메시지 리스트 복사(동기화)
                    tb.Text = string.Concat(copy);                  // 전체 메시지를 RichTextBox에 반영
                }
                else                                                // 그 외 RichTextBox인 경우
                {
                    tb.AppendText(text);                            // 텍스트를 바로 추가
                }
                LineLimit(tb, maxLines);                            // 최대 라인 수 초과 시 앞부분 삭제
            });
        }

        // MQTT 모니터 메시지 버퍼를 MonitorMessage_Add로 UI 출력
        private void MonitorBuffer_Flush()
        {
            // 한 번에 처리할 최대 메시지 수 제한
            const int maxFlushCount = 50;

            List<(string topic, string payload)> toFlush;
            lock (monitorMsgLock)
            {
                if (monitorMsgBuffer.Count == 0) return;

                // 버퍼 크기가 너무 크면 오래된 메시지 버림
                if (monitorMsgBuffer.Count > maxFlushCount * 2)
                {
                    var tempList = new List<(string, string)>(monitorMsgBuffer);
                    monitorMsgBuffer.Clear();
                    // 최신 메시지 maxFlushCount*2개만 유지
                    monitorMsgBuffer.AddRange(tempList.Skip(tempList.Count - maxFlushCount * 2));
                    SystemMessage_Add($"메시지 버퍼 초과: {tempList.Count}개 중 {tempList.Count - maxFlushCount * 2}개 메시지 버림",
                        Color.OrangeRed, true, false);
                }

                // 최대 처리 개수 제한
                int takeCount = Math.Min(monitorMsgBuffer.Count, maxFlushCount);
                toFlush = new List<(string topic, string payload)>(monitorMsgBuffer.Take(takeCount));

                // 처리한 메시지는 제거
                monitorMsgBuffer.RemoveRange(0, takeCount);
            }

            foreach (var (topic, payload) in toFlush)
            {
                MonitorMessage_Add(topic, payload);
            }
        }

        // MQTT 모니터 메시지(토픽, 페이로드) 출력, 라인 제한 적용, 색상 버퍼 관리
        private void MonitorMessage_Add(string topic, string payload)
        {
            try
            {
                string formattedPayload = payload;                                        // JSON 포맷 변환 결과(기본값: 원본)
                try
                {
                    var jsonObj = System.Text.Json.JsonDocument.Parse(payload);           // 페이로드 JSON 파싱 시도
                    formattedPayload = System.Text.Json.JsonSerializer.Serialize(
                        jsonObj,
                        new System.Text.Json.JsonSerializerOptions { WriteIndented = false }
                    );
                }
                catch { }                                                                 // JSON 파싱 실패 시 원본 사용

                var messageBuffer = MonitorMessage_Build(topic, formattedPayload);        // 색상 메시지 버퍼 생성

                lock (mqttColorMessages)                                                  // 색상 메시지 버퍼 동기화
                {
                    string allBufferText = string.Concat(
                        mqttColorMessages.SelectMany(msgList => msgList.Select(x => x.Text))
                    );                                                                    // 전체 메시지 텍스트 결합
                    int totalLines = allBufferText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).Length; // 전체 라인 수 계산

                    string newMsgText = string.Concat(messageBuffer.Select(x => x.Text)); // 신규 메시지 텍스트 결합
                    int newMsgLines = newMsgText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).Length;   // 신규 메시지 라인 수 계산

                    int beforeCount = mqttColorMessages.Count;                            // 삭제 전 버퍼 개수

                    while (totalLines + newMsgLines > mqttLimit && mqttColorMessages.Count > 0) // 라인 제한 초과 시 앞부분 삭제
                    {
                        var first = mqttColorMessages[0];                                 // 첫 메시지 버퍼
                        string firstText = string.Concat(first.Select(x => x.Text));      // 첫 메시지 텍스트 결합
                        int firstLines = firstText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).Length; // 첫 메시지 라인 수
                        totalLines -= firstLines;                                         // 전체 라인 수 차감
                        mqttColorMessages.RemoveAt(0);                                    // 앞부분 삭제
                    }

                    int afterCount = mqttColorMessages.Count;                             // 삭제 후 버퍼 개수

                    mqttColorMessages.Add(messageBuffer);                                 // 신규 메시지 추가

                    MonitorBufferCheck(beforeCount, afterCount, totalLines, newMsgLines); // 버퍼 상태 진단
                    DiagnosticsLog("MonitorMessage_Add 버퍼 처리 후");                    // 진단 로그 기록
                }

                RunOnUIThread(rtbMQTTmonitor, () =>
                {
                    rtbMQTTmonitor.SuspendLayout();                                       // 레이아웃 중지

                    if (rtbMQTTmonitor.Lines.Length > mqttLimit)                          // 라인 제한 초과 시 앞부분 삭제
                    {
                        int removeCount = rtbMQTTmonitor.Lines.Length - mqttLimit;        // 삭제할 라인 수
                        var lines = rtbMQTTmonitor.Lines;                                 // 전체 라인 배열
                        string newText = string.Join("\r\n", lines.Skip(removeCount));    // 앞부분 삭제 후 텍스트 결합
                        rtbMQTTmonitor.Text = newText;                                    // 텍스트 반영
                    }

                    foreach (var (text, color) in messageBuffer)                          // 메시지 버퍼 출력(색상 적용)
                    {
                        // MQTT 모니터(RichTextBox)가 포커스된 상태에서 메시지 수신 시 파싱 규칙 입력란으로 포커스 이동 및 커서 위치 조정
                        if (rtbMQTTmonitor.Focused)                                       // MQTT 모니터가 포커스된 경우
                        {
                            tbParsingText.Focus();                                        // 파싱 규칙 입력란으로 포커스 이동
                            tbParsingText.SelectionStart = tbParsingText.TextLength;      // 커서를 텍스트 끝으로 이동
                            tbParsingText.SelectionLength = 0;                            // 선택 영역 해제
                        }

                        rtbMQTTmonitor.SelectionStart = rtbMQTTmonitor.TextLength;        // 커서 위치 이동
                        rtbMQTTmonitor.SelectionLength = 0;                               // 선택 영역 해제
                        rtbMQTTmonitor.SelectionColor = color;                            // 색상 적용
                        rtbMQTTmonitor.AppendText(text);                                  // 텍스트 추가
                    }
                    rtbMQTTmonitor.SelectionColor = Color.Black;                          // 색상 초기화
                    rtbMQTTmonitor.ResumeLayout();                                        // 레이아웃 재개
                    ScrollToEnd(rtbMQTTmonitor);                                          // 커서를 마지막으로 이동

                    DiagnosticsLog("MonitorMessage_Add UI 갱신 후");                       // 진단 로그 기록
                });

                RunOnUIThread(this, LineLabels_Update);                                   // 라인 제한 라벨 갱신
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"MonitorMessage_Add(string topic, string payload) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지 알림
            }
        }

        // MQTT 모니터 출력 메시지(토픽, 페이로드, 구분선)의 색상 정보 + 리스트 생성
        private List<(string Text, Color Color)>
            MonitorMessage_Build(string topic, string formattedPayload)
        {
            var buffer = new List<(string Text, Color Color)>();         // 색상 강조 메시지 버퍼 리스트 생성
            buffer.Add(("\r\n" + "[Topic] ", Color.DarkGray));           // 토픽 라벨 텍스트와 색상 추가
            buffer.Add((topic, Color.Black));                            // 실제 토픽 텍스트와 색상 추가
            buffer.Add(("\r\n" + "[Payload]" + "\r\n", Color.DarkGray)); // 페이로드 라벨 및 줄바꿈, 색상 추가
            buffer.AddRange(Json_Color(formattedPayload));               // 페이로드(JSON) 색상 강조 텍스트를 리스트에 추가
            buffer.Add(("\r\n" + "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━", Color.DarkGray)); // 구분선 텍스트와 색상 추가
            return buffer;                                               // 완성된 메시지 리스트 반환
        }

        // JSON 문자열 들여쓰기 + 토큰별 색상 정보 파싱
        private List<(string Text, Color Color)> Json_Color(string json)
        {
            var result = new List<(string, Color)>();                // 결과 리스트 생성
            int idx = 0;                                             // 현재 파싱 위치 인덱스
            int indent = 0;                                          // 들여쓰기 레벨
            int indentSize = 1;                                      // 들여쓰기 단위(공백 개수)
            char indentChar = '\t';                                  // 들여쓰기 문자
            bool newLine = true;                                     // 줄바꿈 상태 플래그

            while (idx < json.Length)                                // 전체 JSON 문자열 순회
            {
                char c = json[idx];                                  // 현재 문자 추출

                if (c == '{' || c == '[')                            // 여는 괄호 처리
                {
                    if (!newLine)                                    // 줄바꿈 상태가 아니면
                        result.Add(("\r\n", Color.Black));           // 줄바꿈 추가
                    result.Add((new string(indentChar, indent * indentSize), Color.Black)); // 들여쓰기만큼 공백 추가
                    result.Add((c.ToString(), Color.Black));         // 괄호 문자 추가
                    indent++;                                        // 들여쓰기 레벨 증가
                    idx++;                                           // 인덱스 증가
                    result.Add(("\r\n", Color.Black));               // 줄바꿈 추가
                    newLine = true;                                  // 줄바꿈 상태로 설정
                }
                else if (c == '}' || c == ']')                       // 닫는 괄호 처리
                {
                    indent = Math.Max(0, indent - 1);                // 들여쓰기 레벨 감소(음수 방지)
                    result.Add(("\r\n", Color.Black));               // 줄바꿈 추가
                    result.Add((new string(indentChar, indent * indentSize), Color.Black)); // 들여쓰기만큼 공백 추가
                    result.Add((c.ToString(), Color.Black));         // 괄호 문자 추가
                    idx++;                                           // 인덱스 증가
                    if (idx < json.Length && json[idx] == ',')       // 닫는 괄호 뒤에 쉼표가 있으면
                    {
                        result.Add((json[idx].ToString(), Color.Black)); // 쉼표 문자 추가
                        idx++;                                       // 인덱스 증가
                        result.Add(("\r\n", Color.Black));           // 줄바꿈 추가
                        newLine = true;                              // 줄바꿈 상태로 설정
                    }
                    else
                    {
                        newLine = false;                             // 줄바꿈 상태 해제
                    }
                }
                else if (c == ',')                                   // 쉼표 처리
                {
                    result.Add((c.ToString(), Color.Black));         // 쉼표 문자 추가
                    idx++;                                           // 인덱스 증가
                    result.Add(("\r\n", Color.Black));               // 줄바꿈 추가
                    newLine = true;                                  // 줄바꿈 상태로 설정
                }
                else if (c == '"')                                   // 문자열 처리
                {
                    if (newLine)                                     // 줄바꿈 상태면
                    {
                        result.Add((new string(indentChar, indent * indentSize), Color.Black)); // 들여쓰기만큼 공백 추가
                        newLine = false;                             // 줄바꿈 상태 해제
                    }
                    int start = idx;                                 // 문자열 시작 위치 저장
                    idx++;                                           // 인덱스 증가
                    while (idx < json.Length)                        // 문자열 끝까지 탐색
                    {
                        if (json[idx] == '\\') idx += 2;             // 이스케이프 문자 처리
                        else if (json[idx] == '"') { idx++; break; } // 문자열 끝(")에서 종료
                        else idx++;                                  // 일반 문자면 인덱스 증가
                    }
                    string str = json.Substring(start, idx - start); // 추출한 문자열 저장
                    bool isKey = false;                              // 키 여부 플래그
                    int temp = idx;                                  // 키 판별용 임시 인덱스
                    while (temp < json.Length && char.IsWhiteSpace(json[temp])) temp++; // 공백 건너뜀
                    if (temp < json.Length && json[temp] == ':') isKey = true; // 다음 문자가 ':'이면 키로 판별

                    result.Add((str, isKey ? Color.RoyalBlue : Color.SeaGreen)); // 키/값에 따라 색상 지정하여 추가
                }
                else if (char.IsDigit(c) || (c == '-' && idx + 1 < json.Length && char.IsDigit(json[idx + 1]))) // 숫자 처리
                {
                    if (newLine)                                     // 줄바꿈 상태면
                    {
                        result.Add((new string(indentChar, indent * indentSize), Color.Black)); // 들여쓰기만큼 공백 추가
                        newLine = false;                             // 줄바꿈 상태 해제
                    }
                    int start = idx;                                 // 숫자 시작 위치 저장
                    idx++;                                           // 인덱스 증가
                    while (idx < json.Length && (char.IsDigit(json[idx]) || json[idx] == '.' || json[idx] == 'e' || json[idx] == 'E' || json[idx] == '+' || json[idx] == '-'))
                        idx++;                                       // 숫자, 소수점, 지수부 등 허용
                    result.Add((json.Substring(start, idx - start), Color.OrangeRed)); // 숫자 색상 지정하여 추가
                }
                else if (json.Substring(idx).StartsWith("true"))     // true 처리
                {
                    if (newLine)                                     // 줄바꿈 상태면
                    {
                        result.Add((new string(indentChar, indent * indentSize), Color.Black)); // 들여쓰기만큼 공백 추가
                        newLine = false;                             // 줄바꿈 상태 해제
                    }
                    result.Add(("true", Color.MediumVioletRed));     // true 색상 지정하여 추가
                    idx += 4;                                        // true 길이만큼 인덱스 증가
                }
                else if (json.Substring(idx).StartsWith("false"))    // false 처리
                {
                    if (newLine)                                     // 줄바꿈 상태면
                    {
                        result.Add((new string(indentChar, indent * indentSize), Color.Black)); // 들여쓰기만큼 공백 추가
                        newLine = false;                             // 줄바꿈 상태 해제
                    }
                    result.Add(("false", Color.MediumVioletRed));    // false 색상 지정하여 추가
                    idx += 5;                                        // false 길이만큼 인덱스 증가
                }
                else if (json.Substring(idx).StartsWith("null"))     // null 처리
                {
                    if (newLine)                                     // 줄바꿈 상태면
                    {
                        result.Add((new string(indentChar, indent * indentSize), Color.Black)); // 들여쓰기만큼 공백 추가
                        newLine = false;                             // 줄바꿈 상태 해제
                    }
                    result.Add(("null", Color.Gray));                // null 색상 지정하여 추가
                    idx += 4;                                        // null 길이만큼 인덱스 증가
                }
                else if (c == ':')                                   // 콜론(:) 처리
                {
                    result.Add((c.ToString(), Color.Black));         // 콜론 문자 추가
                    idx++;                                           // 인덱스 증가
                }
                else if (char.IsWhiteSpace(c))                       // 공백 문자 처리
                {
                    idx++;                                           // 인덱스 증가
                }
                else                                                 // 기타 문자 처리
                {
                    idx++;                                           // 인덱스 증가
                }
            }
            return result;                                           // 색상 강조 텍스트 리스트 반환
        }

        // 시스템/모니터/파싱 로그의 제한 초과 메시지 삭제용 백그라운드 타이머
        private void LogCleaner()
        {
            systemMsgCts = new CancellationTokenSource();            // 시스템 메시지 정리용 취소 토큰 생성

            Task.Run(async () =>                                     // 시스템 메시지 정리 백그라운드 태스크 시작
            {
                while (!systemMsgCts.Token.IsCancellationRequested)  // 취소 요청 전까지 반복
                {
                    bool changed = SystemMessages_Trim();            // 시스템 메시지 라인 제한 초과 메시지 삭제
                    if (changed && rtbSystemMSG != null && !rtbSystemMSG.IsDisposed) // 삭제 발생 및 컨트롤 유효 시
                    {
                        RunOnUIThread(rtbSystemMSG, () =>            // UI 스레드에서 안전하게 실행
                        {
                            lock (SystemMsgMessages)                 // 메시지 리스트 동시 접근 방지
                            {
                                rtbSystemMSG.Text = string.Concat(SystemMsgMessages); // 전체 메시지 반영
                                LineLimit(rtbSystemMSG, systemMsgLimit); // 라인 제한 및 커서 이동 통합
                            }
                        });
                    }
                    await Task.Delay(5000, systemMsgCts.Token);      // 대기 후 취소 토큰 적용
                }
            }, systemMsgCts.Token);

            parsingDataCts = new CancellationTokenSource();          // 파싱 데이터 정리용 취소 토큰 생성

            Task.Run(async () =>                                     // 파싱 데이터 정리 백그라운드 태스크 시작
            {
                while (!parsingDataCts.Token.IsCancellationRequested) // 취소 요청 전까지 반복
                {
                    bool changed = ParsedMessages_Trim();            // 파싱 데이터 라인 제한 초과 메시지 삭제
                    if (changed && rtbParsingData != null && !rtbParsingData.IsDisposed) // 삭제 발생 및 컨트롤 유효 시
                    {
                        rtbParsingData.BeginInvoke(new Action(() =>  // UI 스레드에서 RichTextBox 갱신
                        {
                            lock (LogMessages)                       // 메시지 리스트 동시 접근 방지
                            {
                                rtbParsingData.Text = string.Concat(LogMessages); // 전체 메시지 반영
                                LineLimit(rtbParsingData, parsedLimit); // 라인 제한 및 커서 이동 통합
                            }
                        }));
                    }
                    await Task.Delay(5000, parsingDataCts.Token);    // 대기 후 취소 토큰 적용
                }
            }, parsingDataCts.Token);

            mqttMonitorCts = new CancellationTokenSource();          // MQTT 모니터 정리용 취소 토큰 생성

            Task.Run(async () =>                                     // 파싱 데이터 정리 백그라운드 태스크 시작
            {
                while (!mqttMonitorCts.Token.IsCancellationRequested) // 취소 요청 전까지 반복
                {
                    bool changed = ParsedMessages_Trim();            // 파싱 데이터 라인 제한 초과 메시지 삭제
                    if (changed && rtbParsingData != null && !rtbParsingData.IsDisposed) // 삭제 발생 및 컨트롤 유효 시
                    {
                        rtbParsingData.BeginInvoke(new Action(() =>  // UI 스레드에서 RichTextBox 갱신
                        {
                            lock (LogMessages)                       // 메시지 리스트 동시 접근 방지
                            {
                                rtbParsingData.Text = string.Concat(LogMessages); // 전체 메시지 반영
                                LineLimit(rtbParsingData, parsedLimit); // 라인 제한 및 커서 이동 통합
                            }
                        }));
                    }
                    await Task.Delay(5000, mqttMonitorCts.Token);    // 대기 후 취소 토큰 적용
                }
            }, mqttMonitorCts.Token);
        }

        // SystemMsgMessages 라인 제한 초과 삭제, 삭제 여부 반환
        private bool SystemMessages_Trim()
        {
            bool changed = false;                                             // 삭제 발생 여부 플래그

            lock (SystemMsgMessages)                                          // 시스템 메시지 리스트 동시 접근 방지
            {
                int totalLines = SystemMsgMessages.Sum(                       // 전체 라인 수 계산
                    msg => msg.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Length);

                while (totalLines > systemMsgLimit && SystemMsgMessages.Count > 0) // 라인 제한 초과 시 반복 삭제
                {
                    totalLines -= SystemMsgMessages[0].Split(new[] { "\r\n" }, StringSplitOptions.None).Length; // 삭제할 라인 수 차감
                    SystemMsgMessages.RemoveAt(0);                            // 가장 오래된 메시지 삭제
                    changed = true;                                           // 삭제 발생 플래그 설정
                }
            }
            return changed;                                                   // 삭제 발생 여부 반환
        }

        // LogMessages 라인 제한 초과 삭제, 삭제 여부 반환
        private bool ParsedMessages_Trim()
        {
            bool changed = false;                                             // 삭제 발생 여부 플래그

            lock (LogMessages)                                                // 파싱 데이터 메시지 리스트 동시 접근 방지
            {
                int totalLines = LogMessages.Sum(                             // 전체 라인 수 계산
                    msg => msg.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Length);

                while (totalLines > parsedLimit && LogMessages.Count > 0)     // 라인 제한 초과 시 반복 삭제
                {
                    totalLines -= LogMessages[0].Split(new[] { "\r\n" }, StringSplitOptions.None).Length; // 삭제할 라인 수 차감
                    LogMessages.RemoveAt(0);                                  // 가장 오래된 메시지 삭제
                    changed = true;                                           // 삭제 발생 플래그 설정
                }
            }
            return changed;                                                   // 삭제 발생 여부 반환
        }

        // RichTextBox 라인 제한 초과 삭제, 마지막 커서 위치
        private void LineLimit(RichTextBox rtb, int maxLines)
        {
            if (rtb.Lines.Length > maxLines)                                    // 현재 라인 수가 최대 허용 라인 수를 초과하는지 확인
            {
                int removeCount = rtb.Lines.Length - maxLines;                  // 삭제해야 할 라인 수 계산
                var lines = rtb.Lines;                                          // 현재 모든 라인 배열 참조
                string newText = string.Join("\r\n", lines.Skip(removeCount));  // 앞부분 removeCount만큼 제외하고 나머지 라인만 연결
                rtb.Text = newText;                                             // RichTextBox에 최신 라인만 반영
            }
            ScrollToEnd(rtb);                                                   // 커서를 항상 마지막 위치로 이동시켜 최신 메시지가 보이도록 처리
        }

        // RichTextBox 커서 위치 마지막으로 이동
        private void ScrollToEnd(RichTextBox rtb)
        {
            if (rtb == null || rtb.IsDisposed || !rtb.IsHandleCreated) // 컨트롤이 null이거나 이미 해제되었거나 핸들이 생성되지 않은 경우
                return;                                                // 아무 작업도 하지 않고 함수 종료

            RunOnUIThread(rtb, () =>                                   // UI 스레드에서 안전하게 실행
            {
                rtb.SelectionStart = rtb.TextLength;                   // 커서를 텍스트 끝(마지막) 위치로 이동
                rtb.SelectionLength = 0;                               // 선택 영역을 0으로 설정(선택 해제)
                rtb.ScrollToCaret();                                   // 커서 위치로 스크롤 이동(최신 메시지가 보이도록 함)
            });
        }

        // UI 상태 스냅샷을 가져와 각 행 수 반환
        private (int systemMsgLines, int mqttMonitorLines, int parsingDataLines, int gridRows) GetUiStateSnapshot()
        {
            int systemMsgLines = 0, mqttMonitorLines = 0, parsingDataLines = 0, gridRows = 0; // 각 UI 요소별 라인/행 수 초기화
            if (this.InvokeRequired)                                               // UI 스레드가 아니면
            {
                this.Invoke(new Action(() =>                                       // UI 스레드에서 안전하게 값 조회
                {
                    systemMsgLines = rtbSystemMSG?.Lines.Length ?? 0;              // 시스템 메시지 라인 수
                    mqttMonitorLines = rtbMQTTmonitor?.Lines.Length ?? 0;          // MQTT 모니터 라인 수
                    parsingDataLines = rtbParsingData?.Lines.Length ?? 0;          // 파싱 데이터 라인 수
                    gridRows = parsedRows?.Count ?? 0;                             // 파싱 데이터 그리드 행 수
                }));
            }
            else
            {
                systemMsgLines = rtbSystemMSG?.Lines.Length ?? 0;                  // 시스템 메시지 라인 수
                mqttMonitorLines = rtbMQTTmonitor?.Lines.Length ?? 0;              // MQTT 모니터 라인 수
                parsingDataLines = rtbParsingData?.Lines.Length ?? 0;              // 파싱 데이터 라인 수
                gridRows = parsedRows?.Count ?? 0;                                 // 파싱 데이터 그리드 행 수
            }
            return (systemMsgLines, mqttMonitorLines, parsingDataLines, gridRows); // 튜플로 반환
        }

        #endregion



        #region 3. MQTT 연결/이벤트

        // 입력값 검증 및 MQTT 연결 옵션/토픽 생성. 유효성 실패 시 false 반환
        private bool BuildMqttOptions(out MqttClientOptions options, out string topic)
        {
            try
            {
                options = null!;                                     // 반환할 MQTT 옵션 객체 초기화
                topic = string.Empty;                                // 반환할 토픽 문자열 초기화

                string protocol = lstProtocol.SelectedItem?.ToString()?.Trim().ToLower() ?? "mqtt://"; // 프로토콜 선택값 소문자 변환, 기본값 지정
                string server = tbHost.Text.Trim();                  // 호스트 입력값 공백 제거
                string user = tbUserName.Text.Trim();                // 사용자명 입력값 공백 제거
                string pass = tbPassword.Text.Trim();                // 비밀번호 입력값 공백 제거
                topic = tbTopic.Text.Trim();                         // 토픽 입력값 공백 제거
                int port = 0;                                        // 포트 번호 초기화
                int.TryParse(tbPort.Text.Trim(), out port);          // 포트 입력값 정수 변환, 실패 시 0

                bool hasError = false;                               // 입력값 에러 플래그
                hasError |= InputError_Highlight(tbHost, string.IsNullOrEmpty(server)); // 호스트 입력값 검증 및 에러 표시
                hasError |= InputError_Highlight(tbPort, port <= 0); // 포트 입력값 검증 및 에러 표시
                hasError |= InputError_Highlight(tbUserName, string.IsNullOrEmpty(user)); // 사용자명 입력값 검증 및 에러 표시
                hasError |= InputError_Highlight(tbPassword, string.IsNullOrEmpty(pass)); // 비밀번호 입력값 검증 및 에러 표시
                if (hasError) return false;                          // 하나라도 에러 발생 시 false 반환

                string clientId = Guid.NewGuid().ToString();         // 고유 클라이언트 ID 생성
                var optionsBuilder = new MqttClientOptionsBuilder()  // MQTT 옵션 빌더 생성
                    .WithClientId(clientId)                          // 클라이언트 ID 설정
                    .WithCredentials(user, pass)                     // 사용자명/비밀번호 설정
                    .WithCleanSession();                             // 클린 세션 설정

                if (protocol == "ws://")                             // 웹소켓 프로토콜인 경우
                    optionsBuilder = optionsBuilder.WithWebSocketServer($"{server}:{port}"); // 웹소켓 서버 주소/포트 설정
                else                                                 // 일반 MQTT(TCP)인 경우
                    optionsBuilder = port > 0
                        ? optionsBuilder.WithTcpServer(server, port) // 포트가 있으면 TCP 서버 주소/포트 설정
                        : optionsBuilder.WithTcpServer(server);      // 포트가 없으면 주소만 설정

                options = optionsBuilder.Build();                    // 최종 MQTT 옵션 객체 생성
                return true;                                         // 모든 입력값이 유효하면 true 반환
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"BuildMqttOptions(out MqttClientOptions options, out string topic) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
                options = null!;                                     // 예외 발생 시 options를 null로 설정
                topic = string.Empty;                                // 토픽도 빈 문자열로 설정
                return false;                                        // 유효하지 않은 경우 false 반환
            }
        }

        // MQTT 클라이언트의 이벤트 핸들러를 등록하고 중복 등록을 방지
        private void MQTT_EventsHandler_Register()
        {
            try
            {
                if (mqttClient == null) return;                             // MQTT 클라이언트가 null이면 아무 작업도 하지 않음

                mqttClient.ConnectedAsync -= MQTT_Connected;                // 연결 이벤트 핸들러 중복 등록 방지 위해 기존 핸들러 제거
                mqttClient.DisconnectedAsync -= MQTT_Disconnected;          // 해제 이벤트 핸들러 중복 등록 방지 위해 기존 핸들러 제거
                mqttClient.ApplicationMessageReceivedAsync -= MQTT_Message; // 메시지 수신 핸들러 중복 등록 방지 위해 기존 핸들러 제거

                mqttClient.ConnectedAsync += MQTT_Connected;                // 연결 이벤트 핸들러 등록
                mqttClient.DisconnectedAsync += MQTT_Disconnected;          // 해제 이벤트 핸들러 등록
                mqttClient.ApplicationMessageReceivedAsync += MQTT_Message; // 메시지 수신 핸들러 등록
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"MQTT_EventsHandler_Register() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
            }
        }

        // MQTT 브로커 연결 성공 시, 시스템 메시지 출력 및 완료 Task 반환
        private Task MQTT_Connected(MqttClientConnectedEventArgs args)
        {
            try
            {
                SystemMessage_Add($"{tbHost.Text} 서버에 연결되었습니다.", Color.RoyalBlue, true, false); // 시스템 메시지 리스트에 연결 성공 메시지 추가
                isReconnecting = false;                         // 플래그 설정
                return Task.CompletedTask;                      // 비동기 이벤트 핸들러 완료 Task 반환
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"MQTT_Connected(MqttClientConnectedEventArgs args) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
            }
            return Task.CompletedTask;                          // 예외 발생 시에도 완료 Task 반환
        }

        // MQTT 연결 해제 시, 네트워크 장애/브로커 다운/옵션 미설정 등 처리
        private Task MQTT_Disconnected(MqttClientDisconnectedEventArgs args)
        {
            try
            {
                if (isManualDisconnect)                         // 수동 연결 해제인 경우
                {
                    SystemMessage_Add("사용자에 의해 연결이 종료되었습니다.\t\t\t", Color.Black, true, false); // 시스템 메시지 출력
                    SystemMessage_Add("(아마도?)", rtbSystemMSG.BackColor, false, true);           // 시스템 메시지 출력
                    ConnectionUI_Update(false);                 // UI 상태 해제 처리
                }
                else                                            // 비정상 해제(네트워크 장애 등)
                {
                    if (!isReconnecting)                        // 중복 메시지 방지 플래그 확인
                    {
                        isReconnecting = true;                  // 플래그 설정
                        SystemMessage_Add($"연결 끊김, 재연결 시도({MQTTretryDelayMs / 1000}초 간격)...", Color.OrangeRed, true, false); // 시스템 메시지 출력
                    }

                    if (reconnectCts == null || reconnectCts.IsCancellationRequested) // 재연결 토큰이 없거나 취소된 경우
                    {
                        reconnectCts = new CancellationTokenSource(); // 재연결 토큰 생성
                        MQTT_Reconnect(MQTTretryDelayMs, MQTTmaxRetry); // 재연결 시도
                    }
                }
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"MQTT_Disconnected(MqttClientDisconnectedEventArgs args) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지 출력
                ConnectionUI_Update(false);                     // UI 상태 해제 처리
            }
            return Task.CompletedTask;                          // 비동기 이벤트 핸들러 완료 Task 반환
        }

        // MQTT 재연결 루프. 브로커 장애/네트워크 오류 등 비정상 해제 시 재연결 시도 및 UI 상태 갱신
        private async void MQTT_Reconnect(int retryDelayMs = 3000, int maxRetry = 28800)
        {
            if (reconnectCts == null)                             // 재연결 취소 토큰 없으면
                reconnectCts = new CancellationTokenSource();     // 새 취소 토큰 생성
            var token = reconnectCts.Token;                       // 취소 토큰 참조

            for (int attempt = 1; attempt <= maxRetry; attempt++) // 최대 재시도 횟수만큼 반복
            {
                // 수동 해제 또는 취소 요청 시 즉시 종료
                if (isManualDisconnect || token.IsCancellationRequested) // 수동 해제 또는 취소 요청 확인
                {
                    SystemMessage_Add("재연결 시도 취소", Color.Black, true, false); // 시스템 메시지 출력
                    ConnectionUI_Update(false);                   // UI 상태 해제 처리
                    return;                                       // 함수 종료
                }

                try
                {
                    if (mqttClient != null)                       // 기존 클라이언트 존재 시
                    {
                        mqttClient.Dispose();                     // 리소스 해제
                        mqttClient = null;                        // 참조 해제
                    }

                    if (lastMqttOptions == null || string.IsNullOrEmpty(lastMqttTopic)) // 옵션/토픽 정보 없으면
                    {
                        SystemMessage_Add("재연결 실패: 옵션/토픽 정보 없음", Color.OrangeRed, true, false); // 실패 메시지 출력
                        ConnectionUI_Update(false);               // UI 상태 해제 처리
                        return;                                   // 함수 종료
                    }

                    var factory = new MqttFactory();              // MQTT 팩토리 생성
                    mqttClient = factory.CreateMqttClient();      // 새 클라이언트 생성
                    MQTT_EventsHandler_Register();                // 이벤트 핸들러 등록

                    await mqttClient.ConnectAsync(lastMqttOptions, token); // MQTT 연결 시도
                    if (mqttClient.IsConnected)                   // 연결 성공 시
                    {
                        await mqttClient.SubscribeAsync(lastMqttTopic); // 토픽 구독
                        ConnectionUI_Update(true);                // UI 상태 연결 처리
                        isManualDisconnect = false;               // 수동 해제 플래그 해제
                        reconnectCts = null;                      // 재연결 토큰 해제
                        return;                                   // 함수 종료
                    }
                    else                                          // 연결 실패 시
                    {
                        SystemMessage_Add($"재연결 실패({attempt:N0}회): 브로커 응답 없음", Color.OrangeRed, true, false); // 실패 메시지 출력
                    }
                }
                catch (OperationCanceledException)                // 연결 시도 취소 예외
                {
                    SystemMessage_Add("재연결 시도 취소됨(OperationCanceledException)", Color.Black, true, false); // 취소 메시지 출력
                    ConnectionUI_Update(false);                   // UI 상태 해제 처리
                    return;                                       // 함수 종료
                }
                catch (Exception ex)                              // 기타 예외
                {
                    SystemMessage_Add($"재연결 실패({attempt:N0}/{maxRetry:N0}회): {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 메시지 출력
                }

                try
                {
                    await Task.Delay(retryDelayMs, token);        // 재시도 간격 대기(취소 토큰 적용)
                }
                catch (TaskCanceledException)                     // 대기 중 취소 예외
                {
                    SystemMessage_Add("재연결 대기 중 취소", Color.Black, true, false);      // 취소 메시지 출력
                    ConnectionUI_Update(false);                   // UI 상태 해제 처리
                    return;                                       // 함수 종료
                }
            }

            if (!token.IsCancellationRequested && !isManualDisconnect) // 최대 재시도 초과 및 미취소/비수동 해제 시
            {
                SystemMessage_Add($"재연결 시도 종료: 최대 재시도 횟수({maxRetry:N0}회) 초과.", Color.OrangeRed, true, false); // 종료 메시지 출력
                ConnectionUI_Update(false);                       // UI 상태 해제 처리
            }
        }

        // 수신 메시지 토픽과 페이로드 파싱 및 모니터에 출력(MQTT 브로커 메시지 수신 시 호출)
        private Task MQTT_Message(MqttApplicationMessageReceivedEventArgs args)
        {
            try
            {
                string payload = args.ApplicationMessage?.PayloadSegment.Count > 0     // 페이로드가 1바이트 이상이면
                ? System.Text.Encoding.UTF8.GetString(args.ApplicationMessage.PayloadSegment) // UTF8 문자열로 변환
                : ""; // 빈 문자열 반환

                MQTT_Text_Handle(                                                      // 수신 메시지 파싱 및 모니터 출력 메서드 호출
                    $"수신: Topic={args.ApplicationMessage?.Topic}, Payload={payload}" // 토픽과 페이로드를 문자열로 결합
                );
                DiagnosticsLog("MQTT 메시지 수신");                                    // 진단 로그에 메시지 수신 기록
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"MQTT_Message(MqttApplicationMessageReceivedEventArgs args) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false);    // 예외 발생 시 시스템 메시지로 알림
            }
            return Task.CompletedTask;                                                 // 비동기 이벤트 핸들러 완료 Task 반환
        }

        // 파일 저장 워커 비동기 시작, 큐에 쌓인 파싱 데이터 파일 기록
        private void FileWriterStart()
        {
            try
            {
                fileWriteCts = new CancellationTokenSource();           // 파일 저장 취소 토큰 소스 생성(중복 실행 방지)
                Task.Run(async () =>                                    // 비동기 워커 태스크 실행
                {
                    while (!fileWriteCts.Token.IsCancellationRequested) // 취소 요청 전까지 반복
                    {
                        DiagnosticsLog("StartFileWriter 루프");         // 진단 로그에 루프 진입 기록

                        if (!isHeaderWritten && fileWriter != null && lastHeaderFields != null && lastHeaderFields.Count > 0)
                        {
                            string headerLine = string.Join("\t", lastHeaderFields) + "\r\n"; // 헤더 라인 생성
                            fileWriter.Write(headerLine);               // 헤더 파일에 기록
                            fileWriter.Flush();                         // 버퍼 즉시 반영
                            isHeaderWritten = true;                     // 헤더 기록 플래그 설정
                        }

                        while (fileWriteQueue.TryDequeue(out var line)) // 파일 저장 큐에서 데이터가 있으면 반복
                        {
                            if (fileWriter == null) break;              // Writer가 해제되면 즉시 중단
                            fileWriter.Write(line);                     // 파일에 한 줄씩 기록
                        }
                        fileWriter?.Flush();                            // 파일 버퍼를 즉시 디스크에 반영
                        SplitLogFile();                                 // 로그 파일 크기 초과 또는 헤더 변경 시 파일 분할 처리
                        await Task.Delay(100, fileWriteCts.Token);      // 100ms 대기(취소 토큰 적용)
                    }
                }, fileWriteCts.Token);                                 // 취소 토큰을 태스크에 전달
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"FileWriterStart() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
            }
        }

        // 로그 파일 크기 초과 또는 헤더 변경 시 새 파일 교체
        private void SplitLogFile()
        {
            // filePath가 null 또는 빈 문자열이면 즉시 리턴
            if (string.IsNullOrEmpty(ParsingDataFilePath))
            {
                SystemMessage_Add($"[", Color.Black, true, false);
                SystemMessage_Add($"알림", Color.Blue, false, true);
                SystemMessage_Add($"] ", Color.Black, false, true);
                SystemMessage_Add("filePath가 null 또는 빈 문자열입니다. 파일 분할을 건너뜁니다.", Color.RoyalBlue, false, false);
                return;
            }

            // fileWriter도 null 체크(필요시)
            if (fileWriter == null)
            {
                SystemMessage_Add($"[", Color.Black, true, false);
                SystemMessage_Add($"알림", Color.Blue, false, true);
                SystemMessage_Add($"] ", Color.Black, false, true);
                SystemMessage_Add("fileWriter가 null입니다. 파일 분할을 건너뜁니다.", Color.RoyalBlue, false, false);
                return;
            }

            // fileWriter, filePath, lastHeaderFields 유효성 검사
            if (fileWriter == null || string.IsNullOrEmpty(ParsingDataFilePath))
                return;

            // 헤더 필드가 null 또는 비어 있으면 분할/헤더 비교 로직 건너뜀
            if (lastHeaderFields == null || lastHeaderFields.Count == 0)
                return;

            lock (fileWriteLock)                                                       // 파일 동기화 락
            {
                try
                {
                    [DllImport("kernel32.dll", SetLastError = true)]
                    static extern bool FlushFileBuffers(SafeFileHandle hFile);

                    var fileInfo = new System.IO.FileInfo(ParsingDataFilePath);        // 파일 정보 객체 생성
                    bool headerChanged = false;                                        // 헤더 변경 여부 플래그
                    string? fileHeaderLine = null;                                     // 실제 파일 헤더
                    string? expectedHeaderLine = null;                                 // 기대 헤더

                    //AddSystemMessage($"[파일상태] Exists={fileInfo.Exists}, Length={fileInfo.Length}, ReadOnly={fileInfo.IsReadOnly}, LastWrite={fileInfo.LastWriteTime:yyyy-MM-dd HH:mm:ss.fff}"); // 파일 상태 진단 로그

                    fileWriter.Flush();                                                // 버퍼 디스크 반영
                    //System.Threading.Thread.Sleep(2);                                  // flush 후 디스크 반영 대기
                    var fs = (FileStream)fileWriter.BaseStream;                        // 기본 스트림(FileStream) 참조
                    fs.Flush(true);                                                    // OS 버퍼까지 강제 반영
                    FlushFileBuffers(fs.SafeFileHandle);                               // 커널 버퍼까지 강제 반영
                    fileInfo.Refresh();                                                // 파일 정보 갱신

                    //AddSystemMessage($"[파일분할진단] 파일크기={fileInfo.Length}, 분할기준={MaxLogFileSize}, 헤더변경={headerChanged}");

                    if (lastHeaderFields != null && lastHeaderFields.Count > 0
                        && fileInfo.Exists && fileInfo.Length > 0)
                    {
                        try
                        {
                            bool compareResult = FileHeader_Compare(ParsingDataFilePath, lastHeaderFields, out fileHeaderLine, out expectedHeaderLine); // 헤더 비교
                            headerChanged = !compareResult;                            // 불일치 시 true
                            //AddSystemMessage($"[헤더비교] 파일={filePath}, 크기={fileInfo.Length}, 필드수={lastHeaderFields.Count}"); // 비교 로그
                            //AddSystemMessage($"[헤더비교] 실제헤더=[{fileHeaderLine}] / 기대헤더=[{expectedHeaderLine}] / 일치={(compareResult ? "O" : "X")}"); // 비교 결과 로그
                            if (!compareResult)
                            {
                                // 바이트 단위 차이 분석
                                var fileBytes = System.Text.Encoding.UTF8.GetBytes(fileHeaderLine ?? ""); // 실제 헤더 바이트
                                var expectedBytes = System.Text.Encoding.UTF8.GetBytes(expectedHeaderLine ?? ""); // 기대 헤더 바이트
                                int minLen = Math.Min(fileBytes.Length, expectedBytes.Length); // 최소 길이
                                int diffIdx = -1;                                      // 첫 불일치 인덱스
                                for (int i = 0; i < minLen; i++)
                                    if (fileBytes[i] != expectedBytes[i]) { diffIdx = i; break; } // 불일치 위치 찾기
                                //if (diffIdx >= 0)
                                //{
                                //    AddSystemMessage($"[헤더비교] 첫 불일치 인덱스={diffIdx}, 실제=0x{fileBytes[diffIdx]:X2}, 기대=0x{expectedBytes[diffIdx]:X2}"); // 바이트 차이 로그
                                //}
                                //AddSystemMessage($"[헤더비교] 실제헤더(HEX)={BitConverter.ToString(fileBytes)}"); // 실제 헤더 HEX
                                //AddSystemMessage($"[헤더비교] 기대헤더(HEX)={BitConverter.ToString(expectedBytes)}"); // 기대 헤더 HEX
                            }

                            // 헤더 불일치 감지 시 1회 재시도(2ms 대기 후)
                            if (headerChanged)
                            {
                                System.Threading.Thread.Sleep(2);                      // 재시도 전 대기
                                compareResult = FileHeader_Compare(ParsingDataFilePath, lastHeaderFields, out fileHeaderLine, out expectedHeaderLine); // 재비교
                                headerChanged = !compareResult;                        // 재비교 결과 반영
                                //AddSystemMessage($"[헤더비교-재시도] 파일헤더=[{fileHeaderLine}] / 기대헤더=[{expectedHeaderLine}] / 일치={(compareResult ? "O" : "X")}"); // 재비교 로그
                            }
                        }
                        catch (Exception ex)
                        {
                            SystemMessage_Add($"SplitLogFile() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false);  // 예외 발생 시 시스템 메시지로 알림
                        }
                    }

                    //AddSystemMessage($"[파일분할진단] 파일크기={fileInfo.Length}, 분할기준={MaxLogFileSize}, 헤더변경={headerChanged}"); // 분할 조건 진단 로그

                    // race condition/외부 요인 진단 로그
                    if (!fileInfo.Exists)                                              // 파일 없음 경고
                    {
                        SystemMessage_Add($"[", Color.Black, true, false);
                        SystemMessage_Add($"경고", Color.Red, false, true);
                        SystemMessage_Add($"] ", Color.Black, false, true);
                        SystemMessage_Add("데이터 파일 없음(외부 삭제/이동 가능성)", Color.OrangeRed, false, true);
                    }
                    if (fileInfo.Length == 0)                                          // 파일 크기 0 경고
                    {
                        SystemMessage_Add($"[", Color.Black, true, false);
                        SystemMessage_Add($"경고", Color.Red, false, true);
                        SystemMessage_Add($"] ", Color.Black, false, true);
                        SystemMessage_Add("데이터 파일 크기 0(외부에서 내용 삭제/초기화 가능성)", Color.OrangeRed, false, true);
                    }
                    if (fileInfo.IsReadOnly)                                           // 읽기전용 경고
                    {
                        SystemMessage_Add($"[", Color.Black, true, false);
                        SystemMessage_Add($"경고", Color.Red, false, true);
                        SystemMessage_Add($"] ", Color.Black, false, true);
                        SystemMessage_Add("데이터 파일 읽기전용(외부에서 속성 변경 가능성)", Color.OrangeRed, false, true);
                    }

                    if (fileInfo.Length >= MaxLogFileSize || headerChanged)
                    {
                        try
                        {
                            //AddSystemMessage("[분할] 파일 분할 조건 충족, 분할 시작"); // 분할 시작 로그
                            // fileWriter가 null이 아닌지 한 번 더 확인
                            if (fileWriter != null)
                            {
                                fileWriter.Close(); // Writer 닫기
                                fileWriter.Dispose(); // Writer 해제
                                fileWriter = null; // 참조 해제(중복 해제 방지)
                            }

                            if (!string.IsNullOrEmpty(ParsingDataFilePath))
                            {
                                string dir = System.IO.Path.GetDirectoryName(ParsingDataFilePath)!;   // 폴더 경로 추출
                                string name = System.IO.Path.GetFileNameWithoutExtension(ParsingDataFilePath); // 파일명 추출
                                string ext = System.IO.Path.GetExtension(ParsingDataFilePath);        // 확장자 추출

                                try
                                {
                                    // 분할 시점의 현재 시각으로 타임스탬프 생성(밀리초 포함)
                                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff"); // 타임스탬프(밀리초까지)

                                    //// 분할 시점의 타임스탬프 대신, 현재 쓰고 있는 파일의 생성 시각을 기준으로 타임스탬프 생성
                                    //var fi = new System.IO.FileInfo(filePath);         // 파일 정보 객체 생성
                                    //string timestamp = fi.CreationTime.ToString("yyyyMMdd_HHmmss_fff"); // 파일 생성 시각(밀리초까지)으로 타임스탬프 생성

                                    string backupFile = System.IO.Path.Combine(dir, $"{name}_{timestamp}{ext}"); // 백업 파일명

                                    // 파일명 중복 방지(동일 타임스탬프 파일이 이미 있으면 뒤에 _1, _2 ... 추가)
                                    int count = 1;
                                    while (System.IO.File.Exists(backupFile))
                                    {
                                        backupFile = System.IO.Path.Combine(dir, $"{name}_{timestamp}_{count}{ext}");
                                        count++;
                                    }

                                    System.IO.File.Move(ParsingDataFilePath, backupFile); // 파일 이동(백업)
                                }
                                catch (Exception ex)
                                {
                                    SystemMessage_Add($"SplitLogFile() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false);  // 예외 발생 시 시스템 메시지로 알림
                                }

                                fileWriter = new StreamWriter(ParsingDataFilePath, append: false, System.Text.Encoding.UTF8); // 새 Writer 생성

                                var headerFields = (lastHeaderFields != null && lastHeaderFields.Count > 0)
                                    ? lastHeaderFields
                                    : (dynamicHeaders != null && dynamicHeaders.Count > 0 ? dynamicHeaders : null); // 헤더 필드 결정

                                if (headerFields != null && headerFields.Count > 0)
                                {
                                    string headerLine = string.Join("\t", headerFields) + "\r\n"; // 헤더 라인 생성
                                    fileWriter.Write(headerLine);                      // 헤더 기록
                                    fileWriter.Flush();                                // 초기화
                                    System.Threading.Thread.Sleep(2);                  // 헤더 기록 후 디스크 반영 대기

                                    // 헤더 기록 후 실제 파일 첫 줄과 비교(정상 기록 보장)
                                    bool headerOk = false;
                                    try
                                    {
                                        headerOk = FileHeader_Compare(ParsingDataFilePath, headerFields, out fileHeaderLine, out expectedHeaderLine); // 헤더 비교
                                                                                                                                                      //AddSystemMessage($"[헤더기록후비교] 파일헤더=[{fileHeaderLine}] / 기대헤더=[{expectedHeaderLine}] / 일치={(headerOk ? "O" : "X")}"); // 비교 로그
                                        if (!headerOk)
                                        {
                                            System.Threading.Thread.Sleep(2);          // 재시도 전 대기
                                            headerOk = FileHeader_Compare(ParsingDataFilePath, headerFields, out fileHeaderLine, out expectedHeaderLine); // 재비교
                                                                                                                                                          //AddSystemMessage($"[헤더기록후비교-재시도] 파일헤더=[{fileHeaderLine}] / 기대헤더=[{expectedHeaderLine}] / 일치={(headerOk ? "O" : "X")}"); // 재비교 로그
                                        }
                                        if (!headerOk)
                                        {
                                            //AddSystemMessage("[경고] 헤더 기록 후 파일 첫 줄이 일치하지 않음(디스크 지연 가능)"); // 불일치 경고
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        var e = ex;
                                        //AddSystemMessage($"[예외] 헤더 기록 후 비교 실패: {ex.GetType().Name} {ex.Message}"); // 예외 발생 시 로그
                                    }
                                    isHeaderWritten = true;                            // 헤더 기록 플래그 설정
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            SystemMessage_Add($"SplitLogFile() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false);  // 예외 발생 시 시스템 메시지로 알림
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemMessage_Add($"SplitLogFile() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false);  // 예외 발생 시 시스템 메시지로 알림
                }
            }
        }

        // 파일 헤더 기록 및 헤더 불일치 시 파일 분할
        private void FileHeader_Check()
        {
            if (fileWriter == null || string.IsNullOrEmpty(ParsingDataFilePath) || lastHeaderFields == null || lastHeaderFields.Count == 0)
                return; // Writer, 경로, 헤더 필드 없으면 즉시 반환

            var fileInfo = new System.IO.FileInfo(ParsingDataFilePath);           // 파일 정보 객체 생성
            if (!fileInfo.Exists || fileInfo.Length == 0)
            {
                // 새 파일이거나 비어 있으면 헤더 기록
                string headerLine = string.Join("\t", lastHeaderFields) + "\r\n"; // 헤더 라인 생성
                fileWriter.Write(headerLine);                                     // 헤더 파일에 기록
                fileWriter.Flush();                                               // 버퍼 즉시 반영
                isHeaderWritten = true;                                           // 헤더 기록 플래그 설정
            }
            else
            {
                // 기존 파일이면 헤더 비교
                bool headerOk = FileHeader_Compare(ParsingDataFilePath, lastHeaderFields, out _, out _); // 파일 첫 줄과 헤더 비교
                if (!headerOk)
                {
                    // 헤더 불일치 시 파일 분할 및 새 파일 생성
                    SplitLogFile();                                               // 파일 분할 처리
                }
                isHeaderWritten = true;                                           // 헤더 기록 플래그 설정
            }
        }

        // 파일 헤더와 lastHeaderFields 비교
        private bool FileHeader_Compare(string filePath, List<string> lastHeaderFields, out string? fileHeader, out string? expectedHeader)
        {
            fileHeader = null;                                         // 실제 파일 헤더 문자열 초기화
            expectedHeader = null;                                     // 기대 헤더 문자열 초기화
            if (string.IsNullOrEmpty(filePath) || lastHeaderFields == null || lastHeaderFields.Count == 0)
                return false;                                          // 경로/헤더 필드 없으면 false 반환

            int retry = 0;                                             // 재시도 횟수 카운터
            const int maxRetry = 10;                                   // 최대 재시도 횟수(10회)
            const int retryDelayMs = 5;                                // 재시도 간 대기(ms)
            Exception? lastEx = null;                                  // 마지막 예외 저장 변수

            while (retry < maxRetry)                                   // 최대 재시도 횟수까지 반복
            {
                try
                {
                    using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) // 파일 읽기 스트림 생성
                    using (var reader = new StreamReader(fs, System.Text.Encoding.UTF8, true, 1024, leaveOpen: true)) // UTF8 스트림리더 생성
                    {
                        string? firstLine = reader.ReadLine();         // 파일 첫 줄(헤더) 읽기
                        string currentHeader = string.Join("\t", lastHeaderFields); // 기대 헤더 문자열 생성

                        if (firstLine == null)                         // 첫 줄이 없으면(빈 파일 등)
                        {
                            retry++;                                   // 재시도 횟수 증가
                            if (retry < maxRetry)
                                System.Threading.Thread.Sleep(retryDelayMs); // 재시도 전 대기
                            continue;                                  // 다음 반복
                        }

                        string fileHeaderNorm = firstLine.Trim().Replace("\r", "").Replace("\n", ""); // 파일 헤더 정규화(공백/개행 제거)
                        string currentNorm = currentHeader.Trim().Replace("\r", "").Replace("\n", ""); // 기대 헤더 정규화
                        fileHeader = fileHeaderNorm;                   // 실제 파일 헤더 반환값에 저장
                        expectedHeader = currentNorm;                  // 기대 헤더 반환값에 저장
                        return fileHeaderNorm.Equals(currentNorm, StringComparison.Ordinal); // 완전 일치 여부 반환
                    }
                }
                catch (Exception ex)
                {
                    lastEx = ex;                                       // 예외 발생 시 마지막 예외 저장
                    retry++;                                           // 재시도 횟수 증가
                    if (retry < maxRetry)
                        System.Threading.Thread.Sleep(retryDelayMs);   // 재시도 전 대기
                }
            }

            if (lastEx != null)
            {
                SystemMessage_Add($"[", Color.Black, true, false);
                SystemMessage_Add($"경고", Color.Red, false, true);
                SystemMessage_Add($"] ", Color.Black, false, true);
                SystemMessage_Add($"파일 헤더 읽기 실패: {lastEx.GetType().Name} {lastEx.Message} (재시도 {maxRetry}회 후 실패)", Color.OrangeRed, false, true); // 예외 메시지 출력
            }
            if (fileHeader == null)
            {
                SystemMessage_Add($"[", Color.Black, true, false);
                SystemMessage_Add($"경고", Color.Red, false, true);
                SystemMessage_Add($"] ", Color.Black, false, true);
                SystemMessage_Add("파일 헤더 읽기 실패(빈 문자열 반환, 디스크 캐시 지연/외부 접근 가능성)", Color.OrangeRed, false, true); // 헤더 읽기 실패 메시지 출력
            }
            if (filePath.Contains("OneDrive", StringComparison.OrdinalIgnoreCase))
            {
                SystemMessage_Add($"[", Color.Black, true, false);
                SystemMessage_Add($"경고", Color.Red, false, true);
                SystemMessage_Add($"] ", Color.Black, false, true);
                SystemMessage_Add("동기화 폴더(OneDrive 등) 사용 중, 외부 동기화로 인한 파일 접근 지연/경합 가능", Color.OrangeRed, false, true); // 동기화 폴더 경고
            }
            return false;                                              // 모든 시도 실패 시 false 반환
        }

        #endregion



        #region 4. 파싱/데이터 처리

        // MQTT 메시지 토픽과 페이로드 분리 → 모니터 버퍼에 추가 → 페이로드 파싱 버퍼 저장(미동기)
        private void MQTT_Text_Handle(string message)
        {
            try
            {
                DiagnosticsLog("HandleMqttText 진입");           // 진단 로그 기록

                //// MQTT 모니터(RichTextBox)가 포커스된 상태에서 메시지 수신 시 파싱 규칙 입력란으로 포커스 이동 및 커서 위치 조정
                //tbParsingText.BeginInvoke(new Action(() =>
                //{
                //    if (rtbMQTTmonitor.Focused)                  // MQTT 모니터가 포커스된 경우
                //    {
                //        tbParsingText.Focus();                   // 파싱 규칙 입력란으로 포커스 이동
                //        tbParsingText.SelectionStart = tbParsingText.TextLength; // 커서를 텍스트 끝으로 이동
                //        tbParsingText.SelectionLength = 0;       // 선택 영역 해제
                //    }
                //}));

                int topicIdx = message.IndexOf("Topic=");        // "Topic=" 위치 찾기
                int payloadIdx = message.IndexOf("Payload=");    // "Payload=" 위치 찾기

                if (topicIdx >= 0 && payloadIdx > topicIdx)      // 토픽/페이로드 모두 존재하면
                {
                    string topic = message.Substring(
                        topicIdx + 6,
                        payloadIdx - topicIdx - 7
                    ).Trim();                                    // 토픽 문자열 추출

                    string payload = message.Substring(
                        payloadIdx + 8
                    ).Trim();                                    // 페이로드 문자열 추출

                    lock (monitorMsgLock)                        // 모니터 버퍼 동기화
                    {
                        monitorMsgBuffer.Add((topic, payload));  // 모니터 버퍼에 추가
                    }

                    // 무제한 Task 생성 대신 큐에 추가
                    _parsingQueue.Enqueue(payload);

                    // 파싱 워커가 없으면 시작
                    if (_parsingWorkerTask == null || _parsingWorkerTask.IsCompleted)
                    {
                        StartParsingWorker();
                    }
                }
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"MQTT_Text_Handle(string message) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false);
            }
        }

        // 파싱 워커 태스크 시작, 큐에 쌓인 메시지를 비동기 병렬 처리
        private void StartParsingWorker()
        {
            try
            {
                // 기존 워커 취소 토큰 초기화
                if (_parsingWorkerCts.IsCancellationRequested)                      // 기존 토큰이 취소 요청된 상태면
                {
                    _parsingWorkerCts.Dispose();                                    // 토큰 소스 리소스 해제
                    _parsingWorkerCts = new CancellationTokenSource();              // 새 취소 토큰 소스 생성
                }

                _parsingWorkerTask = Task.Run(async () =>                           // 백그라운드 태스크 비동기 실행
                {
                    try
                    {
                        DiagnosticsLog("파싱 워커 시작");                           // 로그에 워커 시작 기록
                        while (!_parsingWorkerCts.Token.IsCancellationRequested)    // 취소 요청 전까지 반복
                        {
                            // 큐에서 작업 꺼내기
                            if (_parsingQueue.TryDequeue(out var payload))          // 큐에서 메시지 추출 성공 시
                            {
                                // 세마포어로 동시 작업 제한
                                await _parsingSemaphore.WaitAsync(_parsingWorkerCts.Token);  // 세마포어 획득 대기
                                try
                                {
                                    ParseAndBufferJson(payload);                    // 추출한 페이로드 파싱 및 버퍼링
                                }
                                finally
                                {
                                    _parsingSemaphore.Release();                    // 세마포어 반환(자원 해제)
                                }
                            }
                            else
                            {
                                // 큐가 비어있으면 잠시 대기 (CPU 사용량 감소)
                                await Task.Delay(50, _parsingWorkerCts.Token);      // 50ms 대기 후 다시 확인
                            }
                        }
                    }
                    catch (OperationCanceledException)                              // 태스크 취소 예외 발생 시
                    {
                        // 정상 취소 - 무시
                    }
                    catch (Exception ex)                                            // 기타 예외 발생 시
                    {
                        SystemMessage_Add($"StartParsingWorker() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false);  // 시스템 메시지로 알림
                    }
                    finally
                    {
                        DiagnosticsLog("파싱 워커 종료");                           // 로그에 워커 종료 기록
                    }
                }, _parsingWorkerCts.Token);                                        // 취소 토큰 전달
            }
            catch (Exception ex)                                                    // 메소드 실행 중 예외 발생 시
            {
                SystemMessage_Add($"StartParsingWorker() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false);  // 시스템 메시지로 알림
            }
        }

        // JSON 문자열을 파싱 규칙에 따라 처리하고 만족도를 충족한 행만 버퍼에 저장
        private void ParseAndBufferJson(string json)
        {
            try
            {
                DiagnosticsLog("ParseAndBufferJson 진입");                   // 진단 로그 기록

                // 파싱 규칙 및 만족도 값 추출
                string[] rules = null;                                       // 파싱 규칙 배열 초기화
                int satisfaction = 50;                                       // 만족도 기본값 설정(50%)

                if (this.InvokeRequired)                                     // UI 스레드가 아닌 경우
                {
                    this.Invoke(new Action(() =>                             // UI 스레드에서 안전하게 실행
                    {
                        var lines = tbParsingText.Lines;                     // 파싱 규칙 입력란의 텍스트 라인 가져오기
                        var rawRules = lines.Where(r => !string.IsNullOrWhiteSpace(r)).ToArray(); // 빈 줄 제외한 규칙 추출
                        satisfaction = ParseSatisfaction(rawRules, 50);      // 첫 줄에서 만족도 값 추출(없으면 기본값 50)
                        rules = ExtractRules(rawRules);                      // 만족도 제외한 실제 파싱 규칙만 추출
                    }));
                }
                else                                                         // 이미 UI 스레드인 경우
                {
                    var lines = tbParsingText.Lines;                         // 파싱 규칙 입력란의 텍스트 라인 가져오기
                    var rawRules = lines.Where(r => !string.IsNullOrWhiteSpace(r)).ToArray(); // 빈 줄 제외한 규칙 추출
                    satisfaction = ParseSatisfaction(rawRules, 50);          // 첫 줄에서 만족도 값 추출(없으면 기본값 50)
                    rules = ExtractRules(rawRules);                          // 만족도 제외한 실제 파싱 규칙만 추출
                }

                if (rules == null || rules.Length == 0)                      // 파싱 규칙이 없는 경우
                    return;                                                  // 작업 중단

                var rows = ParseJsonRows(json, rules);                       // JSON 문자열을 규칙에 따라 파싱하여 행 목록 생성

                List<Dictionary<string, string>> filteredRows;               // 필터링된 행 목록 선언
                if (satisfaction <= 1)                                       // 만족도가 1% 이하이면 (사실상 필터링 없음)
                {
                    filteredRows = rows;                                     // 모든 행 포함
                }
                else                                                         // 만족도 기준 적용
                {
                    int minCount = Math.Max(1, (int)Math.Ceiling(rules.Length * (satisfaction / 100.0))); // 최소 필요 필드 수 계산
                    filteredRows = rows.Where(row =>
                        rules.Count(h => !string.IsNullOrEmpty(GetRowValue(row, h))) >= minCount // 값이 있는 필드 수가 최소 요구 수 이상인 행만 선택
                    ).ToList();
                }

                if (filteredRows.Count > 0)                                  // 필터링 후 남은 행이 있으면
                {
                    lock (pendingParsedRows)                                 // 파싱 데이터 버퍼 동시 접근 방지
                    {
                        pendingParsedRows.Add(filteredRows);                 // 필터링된 행 목록을 버퍼에 추가
                        pendingParsedHeaders.Add(rules);                     // 헤더(파싱 규칙)도 버퍼에 추가
                    }
                }
            }
            catch (Exception ex)                                             // 예외 발생 시
            {
                SystemMessage_Add($"ParseAndBufferJson(string json) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 내용 시스템 메시지로 출력
            }
        }

        // 파싱 규칙 첫 줄에서 만족도(%) 또는 소수(0.1~1.0) 값을 정수로 변환(1~100, 기본값 50)
        private int ParseSatisfaction(string[] parsingRules, int defaultSatisfaction = 50)
        {
            if (parsingRules == null || parsingRules.Length == 0) // 파싱 규칙 배열이 null이거나 비어 있으면
                return defaultSatisfaction;                       // 기본 만족도 반환

            string first = parsingRules[0].Trim();                // 첫 번째 줄의 문자열 앞뒤 공백 제거
            if (first.EndsWith("%"))                              // %로 끝나면
                first = first.TrimEnd('%');                       // % 문자 제거

            if (int.TryParse(first, out int intValue))            // 정수로 변환 시도
            {
                if (intValue >= 1 && intValue <= 100)             // 1~100 범위면
                    return intValue;                              // 해당 값 반환
            }
            if (double.TryParse(first, out double doubleValue))   // 실수로 변환 시도
            {
                if (doubleValue >= 0.01 && doubleValue <= 1.0)    // 0.01~1.0 범위면
                    return (int)(doubleValue * 100);              // 1~100 범위로 변환하여 반환
            }
            return defaultSatisfaction;                           // 위 조건에 모두 해당하지 않으면 기본값 반환
        }

        // 파싱 규칙 첫 줄 만족도 제외, 실제 파싱 규칙만 반환
        private string[] ExtractRules(string[] parsingRules)
        {
            if (parsingRules == null || parsingRules.Length == 0) return Array.Empty<string>(); // 규칙 배열이 null 또는 비어 있으면 빈 배열 반환
            string firstRule = parsingRules[0].Trim();                                          // 첫 번째 규칙 문자열 앞뒤 공백 제거
            if (firstRule.EndsWith("%")) firstRule = firstRule.TrimEnd('%');                    // %로 끝나면 % 문자 제거
            if (int.TryParse(firstRule, out _) || double.TryParse(firstRule, out _))            // 첫 줄이 정수 또는 실수로 변환 가능하면
                return parsingRules.Skip(1).ToArray();                                          // 첫 줄 제외한 나머지 규칙만 반환
            return parsingRules;                                                                // 첫 줄이 만족도 형식이 아니면 전체 규칙 반환
        }

        // JSON 문자열 파싱 규칙에 따라 Dictionary 리스트 변환, 각 행별 결과 반환
        private List<Dictionary<string, string>> ParseJsonRows(string json, string[] rules)
        {
            var result = new List<Dictionary<string, string>>();                  // 파싱 결과를 저장할 리스트 생성
            if (string.IsNullOrWhiteSpace(json) || rules == null || rules.Length == 0)
                return result;                                                    // 입력값이 없거나 규칙이 없으면 빈 리스트 반환

            try
            {
                var doc = System.Text.Json.JsonDocument.Parse(json);              // JSON 문자열을 파싱하여 JsonDocument 생성
                var root = doc.RootElement;                                       // 최상위 JSON 엘리먼트 추출

                Dictionary<string, string> objectFields = new();                  // object 필드 파싱 결과 저장용 딕셔너리

                // payload 내부 objectJSON 필드가 존재하면 파싱하여 objectFields에 저장
                if (root.TryGetProperty("payload", out var payload) &&
                    payload.TryGetProperty("objectJSON", out var objJsonElem))
                {
                    var objJson = objJsonElem.GetString();                        // objectJSON 문자열 추출
                    if (!string.IsNullOrWhiteSpace(objJson))
                    {
                        try
                        {
                            var objDoc = System.Text.Json.JsonDocument.Parse(objJson); // objectJSON을 파싱
                            foreach (var prop in objDoc.RootElement.EnumerateObject()) // 모든 프로퍼티 순회
                                objectFields[$"object.{prop.Name}"] = prop.Value.ToString(); // object.{필드명} 형태로 저장
                        }
                        catch { }                                                 // 파싱 실패 시 무시
                    }
                }

                // root 내부 object 필드가 존재하면 objectFields에 저장
                if (root.TryGetProperty("object", out var objectElem) && objectElem.ValueKind == System.Text.Json.JsonValueKind.Object)
                {
                    foreach (var prop in objectElem.EnumerateObject())            // 모든 프로퍼티 순회
                        objectFields[$"object.{prop.Name}"] = prop.Value.ToString(); // object.{필드명} 형태로 저장
                }

                Dictionary<string, string> rootFields = new();                    // root 필드 파싱 결과 저장용 딕셔너리
                foreach (var prop in root.EnumerateObject())                      // 최상위 모든 프로퍼티 순회
                {
                    if (prop.Value.ValueKind != System.Text.Json.JsonValueKind.Object &&
                        prop.Value.ValueKind != System.Text.Json.JsonValueKind.Array)
                    {
                        rootFields[$"root.{prop.Name}"] = prop.Value.ToString();  // root.{필드명} 형태로 저장
                    }
                }

                Dictionary<string, string> payloadFields = new();                 // payload 필드 파싱 결과 저장용 딕셔너리
                if (root.TryGetProperty("payload", out var payload2))             // payload 프로퍼티 존재 시
                {
                    foreach (var prop in payload2.EnumerateObject())              // 모든 프로퍼티 순회
                    {
                        if (prop.Value.ValueKind != System.Text.Json.JsonValueKind.Object &&
                            prop.Value.ValueKind != System.Text.Json.JsonValueKind.Array)
                        {
                            payloadFields[$"{prop.Name}"] = prop.Value.ToString(); // payload의 각 필드 값을 결과 행에 저장
                        }
                    }
                }

                List<Dictionary<string, string>> rxRows = new();                  // rxInfo 배열 파싱 결과 저장용 리스트
                System.Text.Json.JsonElement rxInfoArr;                           // rxInfo 배열 엘리먼트 선언

                // payload.rxInfo 또는 root.rxInfo 배열이 존재하면 각 요소를 rxRows에 저장
                if ((root.TryGetProperty("payload", out var payload3) && payload3.TryGetProperty("rxInfo", out rxInfoArr) && rxInfoArr.ValueKind == System.Text.Json.JsonValueKind.Array)
                    || (root.TryGetProperty("rxInfo", out rxInfoArr) && rxInfoArr.ValueKind == System.Text.Json.JsonValueKind.Array))
                {
                    foreach (var rx in rxInfoArr.EnumerateArray())                // rxInfo 배열의 각 요소 순회
                    {
                        var rxDict = new Dictionary<string, string>();            // 각 rxInfo 요소의 필드 저장용 딕셔너리
                        foreach (var prop in rx.EnumerateObject())                // 각 요소의 모든 프로퍼티 순회
                        {
                            if (prop.Value.ValueKind == System.Text.Json.JsonValueKind.Object)
                            {
                                foreach (var sub in prop.Value.EnumerateObject()) // 하위 객체의 모든 프로퍼티 순회
                                    rxDict[$"rxInfo.{prop.Name}.{sub.Name}"] = sub.Value.ToString(); // rxInfo.{필드명}.{서브필드명} 형태로 저장
                            }
                            else
                            {
                                rxDict[$"rxInfo.{prop.Name}"] = prop.Value.ToString(); // rxInfo.{필드명} 형태로 저장
                                rxDict[$"{prop.Name}"] = prop.Value.ToString();   // {필드명} 형태로도 저장
                            }
                        }
                        rxRows.Add(rxDict);                                       // 파싱 결과 리스트에 추가
                    }
                }

                int rowCount = Math.Max(1, rxRows.Count);                         // 결과 행 개수 결정(rxRows가 없으면 1)
                for (int i = 0; i < rowCount; i++)                                // 각 결과 행에 대해 반복
                {
                    var row = new Dictionary<string, string>();                   // 한 행의 결과 저장용 딕셔너리

                    // object, root, payload 필드의 값을 결과 행에 우선 복사
                    foreach (var kv in objectFields)                              // objectFields의 모든 키-값 쌍 순회
                        row[kv.Key] = kv.Value;                                   // object.{필드명} 형태의 값을 결과 행에 저장
                    foreach (var kv in rootFields)                                // rootFields의 모든 키-값 쌍 순회
                        row[kv.Key] = kv.Value;                                   // root.{필드명} 형태의 값을 결과 행에 저장
                    foreach (var kv in payloadFields)                             // payloadFields의 모든 키-값 쌍 순회
                        row[kv.Key] = kv.Value;                                   // payload의 각 필드 값을 결과 행에 저장

                    // rxInfo 필드가 존재하면 해당 인덱스의 rxInfo 값을 결과 행에 덮어씀
                    if (rxRows.Count > i)                                         // rxRows에 현재 인덱스의 데이터가 존재하는지 확인
                    {
                        foreach (var kv in rxRows[i])                             // rxRows[i]의 모든 키-값 쌍 순회
                            row[kv.Key] = kv.Value;                               // rxInfo 관련 값을 결과 행에 덮어씀(동일 키는 우선순위로 덮어쓰기)
                    }

                    foreach (var rule in rules)                                   // 모든 파싱 규칙 순회
                    {
                        string key = rule.Trim();                                 // 규칙 문자열 앞뒤 공백 제거
                        string? value = null;                                     // 추출값 초기화
                        value = value ?? "";

                        if (key.StartsWith("object."))                            // object 필드 규칙
                        {
                            objectFields.TryGetValue(key, out value);             // objectFields에서 값 조회
                        }
                        else if (key.StartsWith("rxInfo."))                       // rxInfo 필드 규칙
                        {
                            if (rxRows.Count > i)
                            {
                                var rxKey = key.Substring("rxInfo.".Length);      // rxInfo. 접두사 제거
                                if (!rxRows[i].TryGetValue(key, out value))       // 전체 경로로 조회
                                    rxRows[i].TryGetValue(rxKey, out value);      // 접두사 없는 키로도 조회
                            }
                            else
                            {
                                value = "";                                       // rxRows가 없으면 빈 값
                            }
                        }
                        else if (key.StartsWith("payload."))                      // payload 필드 규칙
                        {
                            payloadFields.TryGetValue(key.Substring("payload.".Length), out value); // payloadFields에서 값 조회
                        }
                        else if (key.StartsWith("root."))                         // root 필드 규칙
                        {
                            rootFields.TryGetValue(key, out value);               // rootFields에서 값 조회
                        }
                        else                                                      // 접두사 없는 규칙(applicationID 등)
                        {
                            if (!payloadFields.TryGetValue(key, out value) || string.IsNullOrEmpty(value)) // payloadFields에서 먼저 조회
                            {
                                if (!rootFields.TryGetValue($"root.{key}", out value) || string.IsNullOrEmpty(value)) // rootFields에서 조회
                                {
                                    objectFields.TryGetValue(key, out value);     // objectFields에서 조회
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(value))                          // 추출값이 비어 있으면 추가 조회 시도
                        {
                            if (key.StartsWith("object."))                        // object.로 시작하는 규칙이면
                                value = objectFields.GetValueOrDefault(key, "");  // objectFields에서 값 재조회(없으면 빈 문자열)
                            else if (key.StartsWith("rxInfo."))                   // rxInfo.로 시작하는 규칙이면
                            {
                                if (rxRows.Count > i)                             // rxRows에 현재 인덱스가 존재하면
                                {
                                    var rxKey = key.Substring("rxInfo.".Length);  // rxInfo. 접두사 제거한 키 생성
                                    if (!rxRows[i].TryGetValue(key, out value))   // 전체 경로로 값 조회 실패 시
                                        rxRows[i].TryGetValue(rxKey, out value);  // 접두사 없는 키로 값 재조회
                                }
                                else
                                {
                                    value = "";                                   // rxRows가 없으면 빈 문자열 반환
                                }
                            }
                            else if (key.StartsWith("root."))                     // root.로 시작하는 규칙이면
                                value = rootFields.GetValueOrDefault(key, "");    // rootFields에서 값 재조회(없으면 빈 문자열)
                            else                                                  // 그 외(접두사 없는 규칙)
                                value = payloadFields.GetValueOrDefault(key, ""); // payloadFields에서 값 재조회(없으면 빈 문자열)
                        }
                        row[key] = value ?? "";                                   // 결과 딕셔너리에 값 저장(값이 null이면 빈 문자열)
                    }
                    result.Add(row);                                              // 결과 리스트에 행 추가
                }
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"ParseJsonRows(string json, string[] rules) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
            }
            return result;                                                        // 최종 결과 리스트 반환
        }

        // 파싱 데이터 버퍼 처리 및 UI 출력, 그리드뷰와 텍스트박스에 파싱 결과 반영
        private void ParsedRows_Flush()
        {
            DiagnosticsLog("FlushParsedRows 진입");                                        // 진단 로그 기록

            List<List<Dictionary<string, string>>> toFlush;                                // 처리할 데이터 버퍼 선언
            List<string[]> headers;                                                        // 처리할 헤더 배열 선언

            lock (pendingParsedRows)                                                       // 파싱 데이터 버퍼 동시 접근 방지
            {
                if (pendingParsedRows.Count == 0) return;                                  // 처리할 데이터 없으면 종료
                toFlush = new List<List<Dictionary<string, string>>>(pendingParsedRows);   // 버퍼 복사
                headers = new List<string[]>(pendingParsedHeaders);                        // 헤더 복사
                pendingParsedRows.Clear();                                                 // 원본 버퍼 초기화
                pendingParsedHeaders.Clear();                                              // 원본 헤더 초기화
            }

            // UI 스레드에서 모든 업데이트 작업 실행
            RunOnUIThread(this, () =>
            {
                for (int i = 0; i < toFlush.Count; i++)                                    // 모든 파싱 결과 처리
                {
                    var rows = toFlush[i];                                                 // 현재 인덱스의 데이터 행 집합
                    var rules = headers[i];                                                // 현재 인덱스의 헤더 규칙
                    if (rules == null || rules.Length == 0) continue;                      // 규칙 없으면 다음 반복으로

                    // RichTextBox에 파싱 결과 출력
                    try
                    {
                        foreach (var row in rows)                                          // 각 데이터 행 처리
                        {
                            var values = rules.Select(h => GetRowValue(row, h)).ToArray(); // 헤더별 값 추출
                            string line = string.Join("\t", values) + "\r\n";              // 탭으로 구분된 텍스트 생성
                            if (fileWriter != null) fileWriteQueue.Enqueue(line);          // 파일 저장 큐에 추가
                            ParsedText_Add(rtbParsingData, line, parsedLimit);             // 텍스트박스에 출력
                        }
                    }
                    catch (Exception ex)
                    {
                        SystemMessage_Add($"ParsedRows_Flush() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 메시지 출력
                    }

                    // DataGridView에 파싱 결과 출력
                    try
                    {
                        ParsedRowsToGrid(rows, rules);                                     // 그리드뷰에 데이터 출력
                    }
                    catch (Exception ex)
                    {
                        SystemMessage_Add($"ParsedRows_Flush() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 메시지 출력
                    }
                }
            });
        }

        // 파싱된 데이터 DataGridView 동적으로 표시, 컬럼/행 관리 및 최신 유지
        private void ParsedRowsToGrid(List<Dictionary<string, string>> rows, string[] rules)
        {
            try
            {
                dgvParsingData.SuspendLayout();                                // 레이아웃 중지

                if (rules == null || rules.Length == 0 || rows == null || rows.Count == 0)
                    return;                                                    // 규칙/데이터 없으면 종료

                var headers = rules.Select(r => r.Trim()).ToList();            // 헤더 목록 생성(공백 제거)
                bool headerChanged = !lastHeaderFields.SequenceEqual(headers); // 헤더 변경 여부

                // 컬럼 변경 또는 최초 출력 시 컬럼 재생성
                if (headerChanged || dgvParsingData.Columns.Count == 0)
                {
                    dgvParsingData.Columns.Clear();                            // 기존 컬럼 삭제
                    dynamicHeaders.Clear();                                    // 동적 헤더 목록 초기화
                    foreach (var h in headers)
                    {
                        dgvParsingData.Columns.Add(h, h);                      // 컬럼 추가
                        dynamicHeaders.Add(h);                                 // 동적 헤더 목록에 추가
                    }
                    isHeaderWritten = true;                                    // 헤더 출력 여부 갱신
                    lastHeaderFields = new List<string>(headers);              // 마지막 헤더 필드 갱신

                    parsedRows.Clear();                                        // 기존 데이터 삭제
                    dgvParsingData.RowCount = 0;                               // 행 수 초기화
                }

                int maxRows = parsedLimit;                                     // 최대 행 수
                foreach (var row in rows)
                {
                    parsedRows.Add(row);                                       // 데이터 행 추가
                    if (parsedRows.Count > maxRows)
                        parsedRows.RemoveAt(0);                                // 최대 초과 시 앞부분 삭제
                }

                // RowCount 변경 시에만 반영
                if (dgvParsingData.RowCount != parsedRows.Count)
                    dgvParsingData.RowCount = parsedRows.Count;                // 행 수 갱신

                // 스크롤 및 선택 처리
                if (dgvParsingData.Rows.Count > 0)
                {
                    dgvParsingData.FirstDisplayedScrollingRowIndex = dgvParsingData.Rows.Count - 1; // 마지막 행으로 스크롤
                    dgvParsingData.ClearSelection();                           // 선택 해제
                    dgvParsingData.Rows[dgvParsingData.Rows.Count - 1].Selected = true; // 마지막 행 선택
                }
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"ParsedRowsToGrid(List<Dictionary<string, string>> rows, string[] rules) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지 알림
            }
            finally
            {
                dgvParsingData.ResumeLayout();                                 // 레이아웃 재개
            }
        }

        // DataGridView의 VirtualMode에서 셀 값 요청 시 동적으로 데이터 제공
        private void OnGridCellValueNeeded(object? sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                // 행과 열 인덱스 유효성 검사(parsedRows 배열 범위 체크)
                if (e.RowIndex >= 0 && e.RowIndex < parsedRows.Count &&
                    e.ColumnIndex >= 0 && e.ColumnIndex < dynamicHeaders.Count)
                {
                    var row = parsedRows[e.RowIndex];                // 요청 행 인덱스의 데이터 딕셔너리 참조
                    var header = dynamicHeaders[e.ColumnIndex];      // 요청 열 인덱스의 헤더 이름 참조
                    e.Value = GetRowValue(row, header);              // 헤더명으로 해당 행에서 값 조회하여 셀 값 설정
                }
                else
                {
                    e.Value = string.Empty;                          // 유효하지 않은 인덱스면 빈 문자열 반환
                }
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"OnGridCellValueNeeded Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
                e.Value = string.Empty;                              // 예외 발생 시 빈 문자열 반환
            }
        }

        // 폼이 활성화될 때 데이터 버퍼 처리 및 UI 갱신
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);                // 기본 활성화 동작 수행

            RunOnUIThread(this, () =>
            {         // UI 스레드에서 안전하게 실행
                MonitorBuffer_Flush();          // MQTT 모니터 버퍼 출력
                ParsedRows_Flush();             // 파싱 데이터 버퍼 출력
                dgvParsingData.Refresh();       // 데이터 그리드뷰 화면 갱신
                LineLabels_Update();            // 라인 제한 라벨 갱신
            });
        }

        // Dictionary에서 키 변형(원본, 짧은 키, 접두사, root/object 접두사)으로 값 추출 및 반환
        private string GetRowValue(Dictionary<string, string> row, string header)
        {
            if (row.TryGetValue(header, out var v1) && !string.IsNullOrEmpty(v1))             // 1. 원본 키(header)로 값이 존재하고 비어있지 않으면
                return v1;                                                                    //    해당 값을 바로 반환

            int dotIdx = header.LastIndexOf('.');                                             // 2. 마지막 점(.) 위치를 찾아 접두사와 짧은 키 분리
            if (dotIdx > 0)
            {
                var shortKey = header.Substring(dotIdx + 1);                                  // 2-1. 마지막 점 뒤의 짧은 키 추출
                if (row.TryGetValue(shortKey, out var v2) && !string.IsNullOrEmpty(v2))       // 2-2. 짧은 키로 값이 존재하고 비어있지 않으면
                    return v2;                                                                //      해당 값을 반환

                var prefix = header.Substring(0, dotIdx);                                     // 2-3. 접두사 부분 추출
                var altKey = $"{prefix}.{shortKey}";                                          // 2-4. 접두사+짧은키 조합 생성
                if (row.TryGetValue(altKey, out var v3) && !string.IsNullOrEmpty(v3))         // 2-5. 조합 키로 값이 존재하고 비어있지 않으면
                    return v3;                                                                //      해당 값을 반환
            }

            if (row.TryGetValue($"root.{header}", out var v4) && !string.IsNullOrEmpty(v4))   // 3. root. 접두사 키로 값이 존재하고 비어있지 않으면
                return v4;                                                                    //    해당 값을 반환

            if (row.TryGetValue($"object.{header}", out var v5) && !string.IsNullOrEmpty(v5)) // 4. object. 접두사 키로 값이 존재하고 비어있지 않으면
                return v5;                                                                    //    해당 값을 반환

            return "";                                                                        // 5. 어떤 경우에도 값이 없으면 빈 문자열 반환
        }

        // JSON 엘리먼트의 모든 하위 필드 재귀적 탐색, Dictionary [경로]=값 형태 저장
        private void ExtractJsonFields(System.Text.Json.JsonElement element, Dictionary<string, string> dict, string prefix)
        {
            try
            {
                if (element.ValueKind == System.Text.Json.JsonValueKind.Object)     // 현재 엘리먼트가 객체인 경우
                {
                    foreach (var prop in element.EnumerateObject())                 // 객체의 모든 프로퍼티 순회
                        ExtractJsonFields(                                          // 하위 엘리먼트에 대해 재귀 호출
                            prop.Value,                                             // 프로퍼티 값 전달
                            dict,                                                   // 결과 저장용 딕셔너리 전달
                            string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}.{prop.Name}"  // 경로가 비어 있으면 프로퍼티명, 아니면 경로+프로퍼티명
                        );
                }
                else if (element.ValueKind == System.Text.Json.JsonValueKind.Array) // 현재 엘리먼트가 배열인 경우
                {
                    int idx = 0;                                                    // 배열 인덱스 변수 선언
                    foreach (var item in element.EnumerateArray())                  // 배열의 모든 요소 순회
                        ExtractJsonFields(                                          // 하위 엘리먼트에 대해 재귀 호출
                            item,                                                   // 배열 요소 전달
                            dict,                                                   // 결과 저장용 딕셔너리 전달
                            $"{prefix}[{idx++}]"                                    // 경로에 인덱스 추가
                        );
                }
                else                                                                // 현재 엘리먼트가 값(문자열, 숫자 등)인 경우
                {
                    dict[prefix] = element.ToString();                              // 경로에 해당하는 값으로 딕셔너리 저장
                }
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"ExtractJsonFields(System.Text.Json.JsonElement element, Dictionary<string, string> dict, string prefix) Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
            }
        }

        // 현재 파싱된 데이터(헤더 및 행) → 탭 구분 텍스트(헤더+데이터) 반환
        public string GetParsedText()
        {
            if (dynamicHeaders.Count == 0 || parsedRows.Count == 0)      // 헤더 또는 데이터가 없으면
                return "";                                               // 빈 문자열 반환

            var sb = new StringBuilder();                                // 결과 문자열 빌더 생성
            sb.AppendLine(string.Join("\t", dynamicHeaders));            // 1행: 헤더를 탭으로 연결하여 추가

            foreach (var row in parsedRows)                              // 각 데이터 행 순회
            {
                var values = dynamicHeaders.Select(h => GetRowValue(row, h)); // 헤더별 값 추출
                sb.AppendLine(string.Join("\t", values));                // 행 데이터를 탭으로 연결하여 추가
            }
            return sb.ToString();                                        // 완성된 텍스트 반환
        }

        #endregion



        #region 5. 메모리/리소스 관리

        // 메모리 모니터링 타이머 생성, 주기적 메모리 사용량 점검
        private void MemoryMonitor_Start()
        {
            if (memoryTimer == null)                           // memoryTimer가 아직 생성되지 않은 경우에만 실행
            {
                memoryTimer = new System.Threading.Timer(_ =>  // 새 System.Threading.Timer 인스턴스 생성
                {
                    try
                    {
                        if (!IsDisposed)                       // 폼이 해제되지 않았는지 확인
                        {
                            RunOnUIThread(this, MemoryTick);   // UI 스레드에서 OnMemoryTick 실행(메모리 점검)
                        }
                    }
                    catch { }                                  // 예외 발생 시 무시(앱 안정성 유지)
                }, null, 0, memoryTimerTick);                  // memoryTimerTick 간격 반복 호출
            }
        }

        // 메모리 사용량 점검, 임계치 초과 시 라인 제한/컨트롤 재생성/MQTT 재연결 등 보호 조치 수행
        private void MemoryTick()
        {
            // Interlocked로 중복 실행 방지
            if (Interlocked.CompareExchange(ref _memoryTickRunning, 1, 0) != 0)
            {
                // 이미 실행 중이면 종료
                return;
            }

            try
            {
                DiagnosticsLog("OnMemoryTick 진입");

                long mem = GC.GetTotalMemory(false);

                if (!isManualDisconnect && mem > CriticalMemoryLevel)
                {
                    MQTT_Reconnect(MQTTretryDelayMs, MQTTmaxRetry);
                }

                long TimeDiff = (long)(DateTime.Now - ResetTime).TotalSeconds;

                if (mem > CriticalMemoryLevel || mem > MaxMemoryUsage || TimeDiff > cls)
                {
                    ResetTime = DateTime.Now;
                    systemMsgLimit = MinSystemMsgLines;
                    mqttLimit = MinMqttLines;
                    parsedLimit = MinParsedLines;
                    SystemMessage_Add($"[", Color.Black, true, false);
                    if (TimeDiff > cls) { SystemMessage_Add($"알림", Color.Red, false, true); }
                    else { SystemMessage_Add($"위험", Color.Red, false, true); }
                    SystemMessage_Add($"] ", Color.Black, false, true);
                    SystemMessage_Add($"(기준:", Color.Black, false, true);
                    SystemMessage_Add($"{CriticalMemoryLevel / (1024 * 1024):N0}MB", Color.OrangeRed, false, true);
                    SystemMessage_Add($"): 메모리 사용량 ", Color.Black, false, true);
                    SystemMessage_Add($"{mem / (1024 * 1024):N0}MB", Color.OrangeRed, false, true);
                    SystemMessage_Add($", 메시지 최대 보관 갯수 조정", Color.Black, false, true);
                    SystemMessage_Add($", 컨트롤 클리어", Color.Black, false, true);
                    if (TimeDiff > cls) { SystemMessage_Add($"({TimeDiff}초 경과)", Color.Red, false, true); }

                    try
                    {
                        ControlClear();

                        ResetCount++;
                        if (ResetCount > 48)
                        {
                            Re_CreatControl();
                            ResetCount = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        SystemMessage_Add($"MemoryTick() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false);
                    }
                }
                else if (mem > WarningMemoryLevel)
                {
                    systemMsgLimit = Math.Max(MinSystemMsgLines, systemMsgLimit / 2);
                    mqttLimit = Math.Max(MinMqttLines, mqttLimit / 2);
                    parsedLimit = Math.Max(MinParsedLines, parsedLimit / 2);
                    SystemMessage_Add($"[", Color.Black, true, false);
                    SystemMessage_Add($"주의", Color.Blue, false, true);
                    SystemMessage_Add($"] ", Color.Black, false, true);
                    SystemMessage_Add($"(기준:", Color.Black, false, true);
                    SystemMessage_Add($"{WarningMemoryLevel / (1024 * 1024):N0}MB", Color.RoyalBlue, false, true);
                    SystemMessage_Add($"): 메모리 사용량 ", Color.Black, false, true);
                    SystemMessage_Add($"{mem / (1024 * 1024):N0}MB", Color.RoyalBlue, false, true);
                    SystemMessage_Add($", 메시지 최대 보관 갯수 조정", Color.Black, false, true);
                }
                else
                {
                    systemMsgLimit = Math.Min(systemMsgLimit + MinSystemMsgLines, MaxSystemMsgLines);
                    mqttLimit = Math.Min(mqttLimit + MinMqttLines, MaxMqttLines);
                    parsedLimit = Math.Min(parsedLimit + MinParsedLines, MaxParsedLines);
                }

                // 불필요한 UI 업데이트 최소화 - 변경 사항이 있을 때만 호출
                LineLabels_Update();
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"MemoryTick() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false);
            }
            finally
            {
                // 플래그 초기화하여 다음 실행 허용
                Interlocked.Exchange(ref _memoryTickRunning, 0);
            }
        }

        //컨트롤 재생성
        private void Re_CreatControl()
        {
            try
            {
                SystemMessage_Add($"컨트롤 재생성", Color.Black, false, false);  // 경고 메시지 출력

                if (rtbMQTTmonitor != null) rtbMQTTmonitor.Dispose();
                rtbMQTTmonitor = new RichTextBox();
                if (rtbParsingData != null) rtbParsingData.Dispose();
                rtbParsingData = new RichTextBox();

                var mqttParent = rtbMQTTmonitor.Parent;               // MQTT 모니터 부모 컨테이너 참조
                var parsedParent = rtbParsingData.Parent;             // 파싱 데이터 부모 컨테이너 참조
                int mqttIndex = -1;                                   // 기본값(찾지 못한 경우 -1)
                int parsedIndex = -1;                                 // 기본값(찾지 못한 경우 -1)
                if (mqttParent != null && rtbMQTTmonitor != null) mqttIndex = mqttParent.Controls.GetChildIndex(rtbMQTTmonitor); // 부모 내 인덱스 저장
                if (parsedParent != null && rtbParsingData != null) parsedIndex = parsedParent.Controls.GetChildIndex(rtbParsingData); // 부모 내 인덱스 저장

                var oldMqttProps = rtbMQTTmonitor != null ? new       // 기존 MQTT 모니터 컨트롤 주요 속성 백업
                {
                    Location = rtbMQTTmonitor.Location,
                    Size = rtbMQTTmonitor.Size,
                    Anchor = rtbMQTTmonitor.Anchor,
                    Dock = rtbMQTTmonitor.Dock,
                    Font = rtbMQTTmonitor.Font,
                    ScrollBars = rtbMQTTmonitor.ScrollBars,
                    Name = rtbMQTTmonitor.Name,
                    TabIndex = rtbMQTTmonitor.TabIndex,
                    ReadOnly = rtbMQTTmonitor.ReadOnly,
                    BackColor = rtbMQTTmonitor.BackColor,
                    ForeColor = rtbMQTTmonitor.ForeColor,
                    BorderStyle = rtbMQTTmonitor.BorderStyle,
                    Enabled = rtbMQTTmonitor.Enabled,
                    Visible = rtbMQTTmonitor.Visible,
                    WordWrap = rtbMQTTmonitor.WordWrap,
                    DetectUrls = rtbMQTTmonitor.DetectUrls,
                    ShortcutsEnabled = rtbMQTTmonitor.ShortcutsEnabled,
                    MaxLength = rtbMQTTmonitor.MaxLength,
                    RightToLeft = rtbMQTTmonitor.RightToLeft,
                    ContextMenuStrip = rtbMQTTmonitor.ContextMenuStrip,
                    AllowDrop = rtbMQTTmonitor.AllowDrop,
                    ImeMode = rtbMQTTmonitor.ImeMode,
                    Padding = rtbMQTTmonitor.Padding,
                    Margin = rtbMQTTmonitor.Margin,
                    AcceptsTab = rtbMQTTmonitor.AcceptsTab,
                    HideSelection = rtbMQTTmonitor.HideSelection,
                    TabStop = rtbMQTTmonitor.TabStop,
                    AutoSize = rtbMQTTmonitor.AutoSize,
                    Tag = rtbMQTTmonitor.Tag,
                    Cursor = rtbMQTTmonitor.Cursor
                } : null;
                var oldParsingProps = rtbParsingData != null ? new    // 기존 파싱 데이터 컨트롤 주요 속성 백업
                {
                    Location = rtbParsingData.Location,
                    Size = rtbParsingData.Size,
                    Anchor = rtbParsingData.Anchor,
                    Dock = rtbParsingData.Dock,
                    Font = rtbParsingData.Font,
                    ScrollBars = rtbParsingData.ScrollBars,
                    Name = rtbParsingData.Name,
                    TabIndex = rtbParsingData.TabIndex,
                    ReadOnly = rtbParsingData.ReadOnly,
                    BackColor = rtbParsingData.BackColor,
                    ForeColor = rtbParsingData.ForeColor,
                    BorderStyle = rtbParsingData.BorderStyle,
                    Enabled = rtbParsingData.Enabled,
                    Visible = rtbParsingData.Visible,
                    WordWrap = rtbParsingData.WordWrap,
                    DetectUrls = rtbParsingData.DetectUrls,
                    ShortcutsEnabled = rtbParsingData.ShortcutsEnabled,
                    MaxLength = rtbParsingData.MaxLength,
                    RightToLeft = rtbParsingData.RightToLeft,
                    ContextMenuStrip = rtbParsingData.ContextMenuStrip,
                    AllowDrop = rtbParsingData.AllowDrop,
                    ImeMode = rtbParsingData.ImeMode,
                    Padding = rtbParsingData.Padding,
                    Margin = rtbParsingData.Margin,
                    AcceptsTab = rtbParsingData.AcceptsTab,
                    HideSelection = rtbParsingData.HideSelection,
                    TabStop = rtbParsingData.TabStop,
                    AutoSize = rtbParsingData.AutoSize,
                    Tag = rtbParsingData.Tag,
                    Cursor = rtbParsingData.Cursor
                } : null;

                if (rtbMQTTmonitor != null) rtbMQTTmonitor.Dispose(); // 기존 MQTT 모니터 컨트롤 해제(널 체크)
                rtbMQTTmonitor = null;                                // 참조 해제
                if (rtbParsingData != null) rtbParsingData.Dispose(); // 기존 파싱 데이터 컨트롤 해제(널 체크)
                rtbParsingData = null;

                if (oldMqttProps != null) rtbMQTTmonitor = new RichTextBox // 새 MQTT 모니터 컨트롤 생성 및 속성 복원
                {
                    Multiline = true,
                    Location = oldMqttProps.Location,
                    Size = oldMqttProps.Size,
                    Anchor = oldMqttProps.Anchor,
                    Dock = oldMqttProps.Dock,
                    Font = oldMqttProps.Font,
                    ScrollBars = oldMqttProps.ScrollBars,
                    Name = oldMqttProps.Name,
                    TabIndex = oldMqttProps.TabIndex,
                    ReadOnly = oldMqttProps.ReadOnly,
                    BackColor = oldMqttProps.BackColor,
                    ForeColor = oldMqttProps.ForeColor,
                    BorderStyle = oldMqttProps.BorderStyle,
                    Enabled = oldMqttProps.Enabled,
                    Visible = oldMqttProps.Visible,
                    WordWrap = oldMqttProps.WordWrap,
                    DetectUrls = oldMqttProps.DetectUrls,
                    ShortcutsEnabled = oldMqttProps.ShortcutsEnabled,
                    MaxLength = oldMqttProps.MaxLength,
                    RightToLeft = oldMqttProps.RightToLeft,
                    ContextMenuStrip = oldMqttProps.ContextMenuStrip,
                    AllowDrop = oldMqttProps.AllowDrop,
                    ImeMode = oldMqttProps.ImeMode,
                    Padding = oldMqttProps.Padding,
                    Margin = oldMqttProps.Margin,
                    AcceptsTab = oldMqttProps.AcceptsTab,
                    HideSelection = oldMqttProps.HideSelection,
                    TabStop = oldMqttProps.TabStop,
                    AutoSize = oldMqttProps.AutoSize,
                    Tag = oldMqttProps.Tag,
                    Cursor = oldMqttProps.Cursor
                };
                if (oldParsingProps != null) rtbParsingData = new RichTextBox // 새 파싱 데이터 컨트롤 생성 및 속성 복원
                {
                    Multiline = true,
                    Location = oldParsingProps.Location,
                    Size = oldParsingProps.Size,
                    Anchor = oldParsingProps.Anchor,
                    Dock = oldParsingProps.Dock,
                    Font = oldParsingProps.Font,
                    ScrollBars = oldParsingProps.ScrollBars,
                    Name = oldParsingProps.Name,
                    TabIndex = oldParsingProps.TabIndex,
                    ReadOnly = oldParsingProps.ReadOnly,
                    BackColor = oldParsingProps.BackColor,
                    ForeColor = oldParsingProps.ForeColor,
                    BorderStyle = oldParsingProps.BorderStyle,
                    Enabled = oldParsingProps.Enabled,
                    Visible = oldParsingProps.Visible,
                    WordWrap = oldParsingProps.WordWrap,
                    DetectUrls = oldParsingProps.DetectUrls,
                    ShortcutsEnabled = oldParsingProps.ShortcutsEnabled,
                    MaxLength = oldParsingProps.MaxLength,
                    RightToLeft = oldParsingProps.RightToLeft,
                    ContextMenuStrip = oldParsingProps.ContextMenuStrip,
                    AllowDrop = oldParsingProps.AllowDrop,
                    ImeMode = oldParsingProps.ImeMode,
                    Padding = oldParsingProps.Padding,
                    Margin = oldParsingProps.Margin,
                    AcceptsTab = oldParsingProps.AcceptsTab,
                    HideSelection = oldParsingProps.HideSelection,
                    TabStop = oldParsingProps.TabStop,
                    AutoSize = oldParsingProps.AutoSize,
                    Tag = oldParsingProps.Tag,
                    Cursor = oldParsingProps.Cursor
                };


                if (rtbSystemMSG != null)
                {
                    rtbSystemMSG.TextChanged += (s, e) => ScrollToEnd(rtbSystemMSG);
                    rtbSystemMSG.Click += rtbSystemMSG_Click;
                }

                if (rtbMQTTmonitor != null)
                {
                    rtbMQTTmonitor.TextChanged += (s, e) => ScrollToEnd(rtbMQTTmonitor);
                }

                if (rtbParsingData != null)
                {
                    rtbParsingData.TextChanged += (s, e) => ScrollToEnd(rtbParsingData);
                }

                if (mqttParent != null && rtbMQTTmonitor != null) mqttParent.Controls.Add(rtbMQTTmonitor);     // 부모 컨테이너에 새 MQTT 모니터 컨트롤 추가
                if (parsedParent != null && rtbParsingData != null) parsedParent.Controls.Add(rtbParsingData);   // 파싱 데이터 부모와 컨트롤이 null이 아니면
                if (mqttParent != null && rtbMQTTmonitor != null) mqttParent.Controls.SetChildIndex(rtbMQTTmonitor, mqttIndex);
                if (parsedParent != null && rtbParsingData != null) parsedParent.Controls.SetChildIndex(rtbParsingData, parsedIndex); // 원래 인덱스 위치로 복원
                if (rtbMQTTmonitor != null) rtbMQTTmonitor.TextChanged += (s, e) => ScrollToEnd(rtbMQTTmonitor); // null이 아니면 텍스트 변경 시 커서 이동
                if (rtbParsingData != null) rtbParsingData.TextChanged += (s, e) => ScrollToEnd(rtbParsingData); // null이 아니면 텍스트 변경 시 커서 이동

                ConnectionUI_Update(false);                           // 연결 상태 UI를 해제 상태로 갱신

                if (isConnected && !isManualDisconnect)               // 연결된 상태이고 수동 해제가 아닌 경우
                {
                    MQTT_Reconnect(MQTTretryDelayMs, MQTTmaxRetry);   // 재연결 시도
                }
            }
            catch (Exception ex)
            {
                SystemMessage_Add($"Re_CreatControl() Exception: {ex.GetType().Name} {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
            }
        }

        // 메모리 모니터링 타이머 중지, 리소스 해제
        private void MemoryMonitor_Stop()
        {
            memoryTimer?.Dispose();        // memoryTimer가 null이 아니면 Dispose로 리소스 해제
            memoryTimer = null;            // 참조를 null로 설정하여 타이머 완전 중지
        }

        #endregion



        #region 성능체크 및 디버그

        // 시스템 메시지 자동복사 기능(디버그용)
        private void rtbSystemMSG_Click(object? sender, EventArgs e)
        {
            if (DebugArgsCheck())                                      // 명령줄 인자가 파싱되었으면
            {
                RunOnUIThread(rtbSystemMSG, () =>                 // UI 스레드에서 안전하게 실행
                {
                    if (!string.IsNullOrEmpty(rtbSystemMSG.Text)) // 텍스트가 비어 있지 않으면
                    {
                        Clipboard.SetText(rtbSystemMSG.Text);     // 시스템 메시지 전체를 클립보드에 복사
                        //AddSystemMessage("시스템 메시지가 클립보드에 복사되었습니다.");
                    }
                });
            }
        }

        // 진단 로그: 메시지/행 수, 메모리, UI 응답성 기록
        private void DiagnosticsLog(string reason = "")
        {
            if (DebugArgsCheck())                                                               // 명령줄 인자가 파싱되었으면
            {
                try
                {
                    long mem = GC.GetTotalMemory(false);                                   // 현재 프로세스 메모리 사용량(바이트)

                    var (systemMsgLines, mqttMonitorLines, parsingDataLines, gridRows) =
                        GetUiStateSnapshot();                                              // UI 상태(메시지/행 수) 스냅샷

                    var sw = System.Diagnostics.Stopwatch.StartNew();                      // UI 응답성 측정용 스톱워치 시작
                    if (this.InvokeRequired)                                               // UI 스레드가 아니면
                        this.Invoke(new Action(() => { }));                                // UI 스레드 roundtrip 측정
                    else
                    { }                                                                    // 이미 UI 스레드면 noop

                    sw.Stop();                                                             // 스톱워치 정지

                    string msg = $"[DIAG] {DateTime.Now:HH:mm:ss.fff} {reason} " +
                        $"Mem={mem / (1024 * 1024)}MB, " +
                        $"SysMsg={systemMsgLines}, MQTT={mqttMonitorLines}, Parse={parsingDataLines}, Grid={gridRows}, " +
                        $"UIInvoke={sw.ElapsedMilliseconds}ms";                            // 진단 메시지 문자열 생성

                    lock (diagLogLock)
                    {
                        string logPath = exePath + "\\Diagnostics.log"; // 진단 로그 파일 경로
                        int retry = 0, maxRetry = 5;
                        while (retry < maxRetry)
                        {
                            try
                            {
                                using (var writer = new StreamWriter(logPath, append: true, System.Text.Encoding.UTF8))
                                {
                                    writer.WriteLine(msg); // 진단 로그 파일에 기록
                                }
                                break; // 성공 시 반복 종료
                            }
                            catch (IOException ex) when (ex.Message.Contains("because it is being used by another process"))
                            {
                                retry++;
                                System.Threading.Thread.Sleep(5); // 50ms 대기 후 재시도
                                if (retry == maxRetry)
                                    SystemMessage_Add($"[DIAG-ERR] {ex.Message}", Color.Red, true, false); // 최대 재시도 후 실패 메시지
                            }
                        }
                    }
                    //SystemMessage_Add(msg, Color.Gray, true, false);                       // 시스템 메시지로 진단 로그 출력
                }
                catch (Exception ex)
                {
                    SystemMessage_Add($"[DIAG-ERR] {ex.Message}", Color.Red, true, false); // 예외 발생 시 시스템 메시지로 알림
                }
            }
        }

        // MQTT 모니터 버퍼 상태 변화(삭제/추가) 진단 메시지 출력
        private void MonitorBufferCheck
            (int beforeCount, int afterCount, int totalLines, int newMsgLines)
        {
            if (DebugArgsCheck())                                              // 명령줄 인자가 파싱되었으면
            {
                if (beforeCount != afterCount)    // 버퍼 개수 변화가 있으면
                    SystemMessage_Add($"MQTT 모니터 버퍼: {beforeCount}->{afterCount}개, " +
                        $"라인수={totalLines}, " +
                        $"신규={newMsgLines}", Color.Red, true, false);   // 진단 메시지 출력
            }
        }

        // 명령줄 인자 파싱 여부로 디버그 모드 활성화 상태 반환
        private bool DebugArgsCheck()
        {
            if (
                argsParsed                                                                 // 기본 명령줄 인자 파싱 여부
                || argsParsedWarningMemoryLevel                                            // 경고 메모리 레벨 인자 파싱 여부
                || argsParsedCriticalMemoryLevel                                           // 임계 메모리 레벨 인자 파싱 여부
                || argsParsedMaxMemoryUsage                                                // 최대 메모리 사용량 인자 파싱 여부
                || argsParsedMaxLogFileSize                                                // 최대 로그 파일 크기 인자 파싱 여부
                || argsParsedMinSystemMsgLines                                             // 최소 시스템 메시지 라인 인자 파싱 여부
                || argsParsedMinMqttLines                                                  // 최소 MQTT 라인 인자 파싱 여부
                || argsParsedMinParsedLines                                                // 최소 파싱 데이터 라인 인자 파싱 여부
                || argsParsedMaxSystemMsgLines                                             // 최대 시스템 메시지 라인 인자 파싱 여부
                || argsParsedMaxMqttLines                                                  // 최대 MQTT 라인 인자 파싱 여부
                || argsParsedMaxParsedLines                                                // 최대 파싱 데이터 라인 인자 파싱 여부
                || argsParsedfilePath                                                      // 파일 경로 인자 파싱 여부
                || argsDebug                                                               // 디버그 모드 활성화 인자 파싱 여부
            )
            {
                return true;                                                               // 하나라도 true면 디버그 모드 활성화
            }
            else
            {
                return false;                                                              // 모두 false면 디버그 모드 비활성화
            }
        }

        #endregion

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