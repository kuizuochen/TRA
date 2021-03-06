﻿Imports System.Collections.ObjectModel

Public Class GlobalVariables
     
    ''Settings
    Public Shared gPreloadDays As Integer = 1

    Public Shared gSchedule As New ObservableCollection(Of clsTrainsInOneDay)

    Public Shared gTrainToShow As clsTrain
    Public Shared gFavoriteToShow As vmStartEndSearch

    Public Shared StationHLColor As String = "Red"
    Public Shared StationColor As String = "White"
    Public Shared StGrpHLColor As String = "Red"
    Public Shared StGrpColor As String = "White"

    Public Shared gZipTimeTableAddress As String = "http://163.29.3.98/xml/45Days.zip"
    Public Shared gZipTimeTableFilePath As String = "shared/transfers/45Days.zip"
    Public Shared gZipTimeTableFileTempPath As String = "shared/transfers/45Days_t.zip"
    Public Shared gXmlFileFolderName As String = "shared/xml/"
    Public Shared gTxtTimeTableFilePaths As List(Of String)
    Public Shared gDefaultXmlFileFolder As String = "Assets/DefaultTimeTable/"

    ''起始設定
    ''Page : 各站列車
    Public Shared gLocationSearchIniStID As Integer = 1008
    ''Page : 區間搜索
    Public Shared gSESearchIniStID_S As Integer = 1008
    Public Shared gSESearchIniStID_E As Integer = 1025

    ''Location Search & pgStSelectionSingle
    Public Shared gUpdateLocSearchSt As Boolean = False
    Public Shared gLocSearchStGrp As clsStGrp = Nothing
    Public Shared gLocSearchSt As clsStation = Nothing

    ''Location Search & pgStSelection
    Public Shared gUpdateSE As Boolean = False
    Public Shared gSEStGrp_Start As clsStGrp = Nothing
    Public Shared gSESt_Start As clsStation = Nothing
    Public Shared gSEStGrp_End As clsStGrp = Nothing
    Public Shared gSESt_End As clsStation = Nothing

    Public Shared gStList As List(Of clsStation) = New List(Of clsStation) From {
New clsStation(1001, "基隆", "Keelung", 1, 25.132302, 121.739466),
New clsStation(1002, "八堵", "Badu", 2, 25.108334, 121.729054),
New clsStation(1003, "七堵", "Cidu", 1, 25.093453, 121.713928),
New clsStation(1004, "五堵", "Wudu", 3, 25.077857, 121.66763),
New clsStation(1005, "汐止", "Sijhih", 2, 25.068316, 121.661735),
New clsStation(1006, "南港", "Nangang", 2, 25.053216, 121.607058),
New clsStation(1007, "松山", "Songshan", 1, 25.049283, 121.578012),
New clsStation(1008, "臺北", "Taipei", 0, 25.047924, 121.517081),
New clsStation(1009, "萬華", "Wanhua", 1, 25.033396, 121.500439),
New clsStation(1011, "板橋", "Banciao", 1, 25.014051, 121.463815),
New clsStation(1032, "浮洲", "Fuzhou", 3, 25.004192, 121.444737),
New clsStation(1012, "樹林", "Shulin", 1, 24.991407, 121.424601),
New clsStation(1013, "山佳", "Shanjia", 3, 24.972481, 121.392614),
New clsStation(1014, "鶯歌", "Yingge", 2, 24.954525, 121.35528),
New clsStation(1015, "桃園", "Taoyuan", 1, 24.989206, 121.313549),
New clsStation(1016, "內壢", "Neili", 3, 24.972769, 121.258212),
New clsStation(1017, "中壢", "Jhongli", 1, 24.953636, 121.225648),
New clsStation(1018, "埔心", "Pusin", 3, 24.919454, 121.183665),
New clsStation(1019, "楊梅", "Yangmei", 3, 24.91402, 121.145963),
New clsStation(1020, "富岡", "Fugang", 3, 24.934427, 121.083067),
New clsStation(1021, "湖口", "Hukou", 3, 24.902865, 121.044123),
New clsStation(1022, "新豐", "Sinfong", 3, 24.869794, 120.996771),
New clsStation(1023, "竹北", "Jhubei", 3, 24.839161, 121.009456),
New clsStation(1024, "北新竹", "North Hsinchu", 3, 24.808765, 120.983824),
New clsStation(1025, "新竹", "Hsinchu", 1, 24.801643, 120.971696),
New clsStation(1026, "香山", "Siangshan", 3, 24.763128, 120.913874),
New clsStation(1027, "崎頂", "Ciding", 4, 24.722879, 120.871892),
New clsStation(1028, "竹南", "Jhunan", 1, 24.686611, 120.880698),
New clsStation(1029, "三坑", "Sankeng", 3, 25.123053, 121.742017),
New clsStation(1030, "百福", "na", -1, 25.077924, 121.693623),
New clsStation(1031, "汐科", "na", -1, 25.062563, 121.64665),
New clsStation(1102, "談文", "Tanwan", 4, 24.656415, 120.858242),
New clsStation(1103, "談文南", "Tanwannan", 8, 24.686611, 120.880698),
New clsStation(1104, "大山", "Dashan", 3, 24.645645, 120.803778),
New clsStation(1105, "後龍", "Houlong", 3, 24.616212, 120.787307),
New clsStation(1106, "龍港", "Longgang", 4, 24.611683, 120.758142),
New clsStation(1107, "白沙屯", "Baishatun", 3, 24.564797, 120.708198),
New clsStation(1108, "新埔", "Sinpu", 3, 24.54018, 120.695179),
New clsStation(1109, "通霄", "Tongsiao", 3, 24.491403, 120.678425),
New clsStation(1110, "苑裡", "Yuanli", 3, 24.443426, 120.651494),
New clsStation(1111, "日南", "Rihnan", 3, 24.378066, 120.654119),
New clsStation(1112, "大甲", "Dajia", 2, 24.34443, 120.627017),
New clsStation(1113, "臺中港", "Taichung Port", 2, 24.304388, 120.602303),
New clsStation(1114, "清水", "Cingshuei", 3, 24.263624, 120.569178),
New clsStation(1115, "沙鹿", "Shalu", 2, 24.237044, 120.557627),
New clsStation(1116, "龍井", "Longjing", 3, 24.197444, 120.543371),
New clsStation(1117, "大肚", "Dadu", 3, 24.154024, 120.542536),
New clsStation(1118, "追分", "Jhuifen", 3, 24.120577, 120.570128),
New clsStation(1119, "大肚溪南", "na", 8, 0, 0),
New clsStation(1120, "彰化", "Changhua", 1, 24.081666, 120.538539),
New clsStation(1202, "花壇", "Huatan", 3, 24.024996, 120.5374),
New clsStation(1203, "員林", "Yuanlin", 1, 23.959346, 120.56996),
New clsStation(1204, "永靖", "Yongjing", 4, 23.928147, 120.571672),
New clsStation(1205, "社頭", "Shetou", 3, 23.89571, 120.5808),
New clsStation(1206, "田中", "Tianjhong", 2, 23.858503, 120.591396),
New clsStation(1207, "二水", "Ershuei", 2, 23.81315, 120.618115),
New clsStation(1208, "林內", "Linnei", 3, 23.759681, 120.614987),
New clsStation(1209, "石榴", "Shihliou", 4, 23.731671, 120.580019),
New clsStation(1210, "斗六", "Douliou", 1, 23.711793, 120.541171),
New clsStation(1211, "斗南", "Dounan", 2, 23.672972, 120.480841),
New clsStation(1212, "石龜", "Shihguei", 4, 23.639509, 120.471049),
New clsStation(1213, "大林", "Dalin", 3, 23.601076, 120.455839),
New clsStation(1214, "民雄", "Minsyong", 3, 23.555039, 120.431651),
New clsStation(1215, "嘉義", "Chiayi", 1, 23.479306, 120.440828),
New clsStation(1217, "水上", "Shueishang", 3, 23.433976, 120.399706),
New clsStation(1218, "南靖", "Nanjing", 3, 23.413433, 120.386539),
New clsStation(1219, "後壁", "Houbi", 3, 23.366247, 120.360603),
New clsStation(1220, "新營", "Sinying", 1, 23.306732, 120.323055),
New clsStation(1221, "柳營", "Liouying", 3, 23.27762, 120.322519),
New clsStation(1222, "林鳳營", "Linfongying", 3, 23.242592, 120.320973),
New clsStation(1223, "隆田", "Longtian", 2, 23.192708, 120.319202),
New clsStation(1224, "拔林", "Balin", 4, 23.172291, 120.321185),
New clsStation(1225, "善化", "Shanhua", 2, 23.133324, 120.306551),
New clsStation(1226, "新市", "Sinshih", 3, 23.068191, 120.290043),
New clsStation(1227, "永康", "Yongkang", 2, 23.038338, 120.253524),
New clsStation(1228, "臺南", "Tainan", 1, 22.997144, 120.212966),
New clsStation(1229, "保安", "Baoan", 3, 22.932952, 120.231635),
New clsStation(1230, "中洲", "Jhongjhou", 2, 22.904026, 120.252898),
New clsStation(1231, "大湖", "Dahu", 3, 22.878186, 120.253942),
New clsStation(1232, "路竹", "Lujhu", 3, 22.853951, 120.266267),
New clsStation(1233, "岡山", "Gangshan", 1, 22.792271, 120.300019),
New clsStation(1234, "橋頭", "Ciaotou", 3, 22.76084, 120.310187),
New clsStation(1235, "楠梓", "Nanzih", 2, 22.727035, 120.324371),
New clsStation(1236, "左營", "Zuoying", 3, 22.675873, 120.295453),
New clsStation(1237, "鼓山", "Gushan", 3, 22.63962, 120.302111),
New clsStation(1238, "高雄", "Kaohsiung", 0, 22.63962, 120.302111),
New clsStation(1239, "大橋", "Daciao", 3, 23.019399, 120.22442),
New clsStation(1240, "大村", "Datsun", 3, 23.990053, 120.560645),
New clsStation(1241, "嘉北", "ChiaPei", 3, 23.499897, 120.448503),
New clsStation(1242, "新左營", "na", 1, 22.687544, 120.306788),
New clsStation(1302, "造橋", "Zaociao", 4, 24.641863, 120.867194),
New clsStation(1304, "豐富", "Fongfu", 4, 24.601129, 120.823549),
New clsStation(1305, "苗栗", "Miaoli", 1, 24.57002, 120.822343),
New clsStation(1307, "南勢", "Nanshih", 4, 24.522481, 120.791544),
New clsStation(1308, "銅鑼", "Tongluo", 3, 24.48634, 120.786173),
New clsStation(1310, "三義", "Sanyi", 3, 24.42062, 120.773931),
New clsStation(1314, "泰安", "Taian", 3, 24.331292, 120.741816),
New clsStation(1315, "后里", "Houli", 3, 24.309312, 120.732893),
New clsStation(1317, "豐原", "Fongyuan", 1, 24.254112, 120.722917),
New clsStation(1318, "潭子", "Tanzih", 3, 24.212825, 120.705572),
New clsStation(1319, "臺中", "Taichung", 0, 24.136781, 120.685008),
New clsStation(1320, "烏日", "Wurih", 3, 24.108659, 120.622443),
New clsStation(1321, "成功", "Chenggong", 3, 24.114232, 120.590164),
New clsStation(1322, "大慶", "Dacing", 3, 24.118876, 120.647897),
New clsStation(1323, "太原", "Taiyuan", 3, 24.166682, 120.700085),
New clsStation(1324, "新烏日", "na", -1, 24.109851, 120.614309),
New clsStation(1402, "鳳山", "Fongshan", 2, 22.631284, 120.357683),
New clsStation(1403, "後庄", "Houjhuang", 3, 22.640163, 120.391313),
New clsStation(1404, "九曲堂", "Jioucyutang", 3, 22.656447, 120.42094),
New clsStation(1405, "六塊厝", "Lioukuaicuo", 4, 22.665987, 120.464996),
New clsStation(1406, "屏東", "Pingtung", 1, 22.669306, 120.486203),
New clsStation(1407, "歸來", "Gueilai", 4, 22.652189, 120.502952),
New clsStation(1408, "麟洛", "Linluo", 4, 22.634819, 120.514368),
New clsStation(1409, "西勢", "Sishih", 8, 22.616433, 120.526697),
New clsStation(1410, "竹田", "Jhutian", 3, 22.586465, 120.539964),
New clsStation(1411, "潮州", "Chaojhou", 3, 22.550086, 120.53642),
New clsStation(1412, "崁頂", "Kanding", 4, 22.513074, 120.514796),
New clsStation(1413, "南州", "Nanjhou", 3, 22.492058, 120.511738),
New clsStation(1414, "鎮安", "Jhenan", 4, 22.457927, 120.511321),
New clsStation(1415, "林邊", "Linbian", 3, 22.431406, 120.515376),
New clsStation(1416, "佳冬", "Jiadong", 3, 22.414087, 120.547742),
New clsStation(1417, "東海", "Donghai", 4, 22.398968, 120.572356),
New clsStation(1418, "枋寮", "Fangliao", 3, 22.368019, 120.595098),
New clsStation(1502, "加祿", "Jialu", 3, 22.330971, 120.624621),
New clsStation(1503, "內獅", "Neishih", 4, 22.306186, 120.643348),
New clsStation(1504, "枋山", "Fangshan", 4, 22.267061, 120.659504),
New clsStation(1505, "枋野", "Fangye", 3, 23.69781, 120.960515),
New clsStation(1506, "中央", "na", -1, 22.62464, 120.301191),
New clsStation(1507, "古莊", "Gujhuang", 3, 22.345509, 120.878079),
New clsStation(1508, "大武", "Dawu", 3, 22.365217, 120.9008),
New clsStation(1510, "瀧溪", "Lunghsi", 3, 22.460981, 120.941792),
New clsStation(1511, "多良", "na", -1, 22.531488, 120.967239),
New clsStation(1512, "金崙", "Jinlun", 3, 22.531488, 120.967239),
New clsStation(1514, "太麻里", "Taimali", 3, 22.618823, 121.005007),
New clsStation(1516, "知本", "Jhihben", 3, 22.710185, 121.06067),
New clsStation(1517, "康樂", "Kangle", 3, 22.764274, 121.093555),
New clsStation(1602, "吉安", "Jian", 3, 23.967538, 121.582029),
New clsStation(1604, "志學", "Jhihsyue", 3, 23.907494, 121.529437),
New clsStation(1605, "平和", "Pinghe", 4, 23.882774, 121.520485),
New clsStation(1606, "壽豐", "Shoufong", 3, 23.869016, 121.510633),
New clsStation(1607, "豐田", "Fongtian", 3, 23.848475, 121.496168),
New clsStation(1608, "溪口", "Sikou", 3, 23.555039, 120.431651),
New clsStation(1609, "南平", "Nanping", 3, 23.782276, 121.45828),
New clsStation(1610, "鳳林", "Fonglin", 3, 23.74634, 121.447024),
New clsStation(1611, "萬榮", "Wanrong", 3, 23.711978, 121.419067),
New clsStation(1612, "光復", "Guangfu", 3, 23.666293, 121.421168),
New clsStation(1613, "大富", "Dafu", 4, 23.605688, 121.389624),
New clsStation(1614, "富源", "Fuyuan", 3, 23.580268, 121.380122),
New clsStation(1616, "瑞穗", "Rueisuei", 3, 23.497376, 121.376841),
New clsStation(1617, "三民", "Sanmin", 3, 23.424535, 121.345403),
New clsStation(1619, "玉里", "Yuli", 1, 23.331518, 121.311726),
New clsStation(1620, "安通", "Antung", 3, 23.331518, 121.311726),
New clsStation(1621, "東里", "Dongli", 3, 23.272309, 121.304181),
New clsStation(1622, "東竹", "Dongjhu", 3, 23.226025, 121.278481),
New clsStation(1623, "富里", "Fuli", 3, 23.179132, 121.248692),
New clsStation(1624, "池上", "Chihshang", 3, 23.126059, 121.219424),
New clsStation(1625, "海端", "Haiduan", 3, 23.102934, 121.176829),
New clsStation(1626, "關山", "Guanshan", 3, 23.045665, 121.164373),
New clsStation(1627, "月美", "Yuemei", -1, 0, 0),
New clsStation(1628, "瑞和", "Rueihe", 4, 22.979985, 121.15584),
New clsStation(1629, "瑞源", "Rueiyuan", 3, 22.955993, 121.158964),
New clsStation(1630, "鹿野", "Luye", 3, 22.912469, 121.137004),
New clsStation(1631, "山里", "Shanli", 3, 22.862046, 121.138031),
New clsStation(1632, "臺東", "Taitung", 1, 22.793711, 121.123161),
New clsStation(1633, "馬(廢)蘭", "na", -1, 23.69781, 120.960515),
New clsStation(1634, "臺(廢)東", "na", -1, 23.69781, 120.960515),
New clsStation(1635, "舞鶴", "Wuhe", 3, 23.497376, 121.376841),
New clsStation(1703, "永樂", "Yongle", 3, 24.568417, 121.844564),
New clsStation(1704, "東澳", "Dongao", 2, 24.518281, 121.830705),
New clsStation(1705, "南澳", "Nanao", 3, 24.463396, 121.800926),
New clsStation(1706, "武塔", "Wuta", 4, 24.448674, 121.776037),
New clsStation(1708, "漢本", "Hanben", 3, 24.335428, 121.768355),
New clsStation(1709, "和平", "Heping", 2, 24.298296, 121.753346),
New clsStation(1710, "和仁", "Horen", 3, 24.242132, 121.712018),
New clsStation(1711, "崇德", "Chongde", 3, 24.172134, 121.655498),
New clsStation(1712, "新城", "Sincheng", 2, 24.127524, 121.640866),
New clsStation(1713, "景美", "Jingmei", 3, 24.090317, 121.610786),
New clsStation(1714, "北埔", "Beipu", 3, 24.032506, 121.601667),
New clsStation(1715, "花蓮", "Hualien", 0, 23.992868, 121.600993),
New clsStation(1802, "暖暖", "Nuannuan", 4, 25.101448, 121.737708),
New clsStation(1803, "四腳亭", "Sihjiaoting", 3, 25.102751, 121.761887),
New clsStation(1804, "瑞芳", "Rueifang", 1, 25.108928, 121.806148),
New clsStation(1805, "侯硐", "Houtung", 3, 25.087009, 121.827424),
New clsStation(1806, "三貂嶺", "Sandiaoling", 3, 25.065544, 121.822559),
New clsStation(1807, "牡丹", "Mudan", 3, 25.058768, 121.851999),
New clsStation(1808, "雙溪", "Shuangsi", 2, 25.038562, 121.866504),
New clsStation(1809, "貢寮", "Gungliao", 3, 25.022063, 121.908687),
New clsStation(1810, "福隆", "Fulong", 3, 25.015921, 121.94478),
New clsStation(1811, "石城", "Shihcheng", 4, 24.978334, 121.945192),
New clsStation(1812, "大里", "Dali", 3, 24.966799, 121.922496),
New clsStation(1813, "大溪", "Dasi", 3, 24.93835, 121.889837),
New clsStation(1814, "龜山", "Gueishan", 3, 24.904818, 121.868878),
New clsStation(1815, "外澳", "Waiao", 4, 24.883703, 121.845758),
New clsStation(1816, "頭城", "Toucheng", 3, 24.858976, 121.822556),
New clsStation(1817, "頂埔", "Dingpu", 4, 24.843998, 121.809207),
New clsStation(1818, "礁溪", "Jiaohsi", 3, 24.827035, 121.775354),
New clsStation(1819, "四城", "Sihcheng", 3, 24.786802, 121.762728),
New clsStation(1820, "宜蘭", "Yilan", 1, 24.754512, 121.758253),
New clsStation(1821, "二結", "Erjie", 3, 24.705267, 121.774131),
New clsStation(1822, "中里", "Jhongli", 4, 24.694166, 121.775256),
New clsStation(1823, "羅東", "Luodong", 2, 24.677929, 121.774629),
New clsStation(1824, "冬山", "Dongshan", 2, 24.636297, 121.792107),
New clsStation(1825, "新馬", "Sinma", 4, 24.615395, 121.8229),
New clsStation(1826, "蘇澳新站", "Suaosin", 2, 24.608858, 121.827604),
New clsStation(1827, "蘇澳", "Suao", 1, 24.595181, 121.85144),
New clsStation(1903, "大華", "Dahua", 4, 25.049964, 121.797847),
New clsStation(1904, "十分", "Shihfen", 3, 25.041153, 121.775163),
New clsStation(1905, "望古", "Wanggu", 4, 25.034496, 121.763751),
New clsStation(1906, "嶺腳", "Lingjiao", 4, 25.030052, 121.74804),
New clsStation(1907, "平溪", "Pingsi", 3, 25.025645, 121.740002),
New clsStation(1908, "菁桐", "Jingtong", 3, 25.02388, 121.723722),
New clsStation(2002, "深澳", "na", -1, 25.108928, 121.806148),
New clsStation(2102, "五福", "na", -1, 24.491403, 120.678425),
New clsStation(2103, "林口", "Linkou", -1, 0, 0),
New clsStation(2104, "電廠", "na", -1, 23.69781, 120.960515),
New clsStation(2105, "桃中", "na", -1, 24.953636, 121.225648),
New clsStation(2106, "寶山", "na", -1, 24.801643, 120.971696),
New clsStation(2107, "南祥", "na", -1, 23.019399, 120.22442),
New clsStation(2108, "長興", "Hengshan", 3, 22.669306, 120.486203),
New clsStation(2109, "海山站", "na", -1, 24.985339, 121.448786),
New clsStation(2110, "海湖站", "na", -1, 23.69781, 120.960515),
New clsStation(2212, "千甲", "Qianjia", 3, 24.806626, 121.003379),
New clsStation(2213, "新莊", "Xinzhuang", 3, 24.788028, 121.021948),
New clsStation(2203, "竹中", "Jhujhong", 3, 24.781296, 121.031297),
New clsStation(2214, "六家", "Liujia", 3, 24.807528, 121.0393),
New clsStation(2204, "上員", "Shangyuan", 4, 24.777831, 121.055699),
New clsStation(2205, "竹東", "Jhudong", 2, 24.738245, 121.094754),
New clsStation(2206, "橫山", "Hengshan", 4, 24.720563, 121.116546),
New clsStation(2207, "九讚頭", "jiouzantou", 3, 24.720638, 121.136215),
New clsStation(2208, "合興", "Hesing", 4, 24.716709, 121.15439),
New clsStation(2209, "富貴", "Fuguei", 4, 24.715543, 121.167371),
New clsStation(2210, "內灣", "Neiwan", 3, 24.705302, 121.182556),
New clsStation(2211, "榮華", "Ronghua", 4, 24.748359, 121.08331),
New clsStation(2302, "臺中港貨", "Taichung Port", 3, 24.2332076, 120.9417368),
New clsStation(2402, "龍井煤場", "Longjing", 3, 23.69781, 120.960515),
New clsStation(2502, "神岡", "Shangang", 3, 24.212825, 120.705572),
New clsStation(2702, "源泉", "Yuanciyuan", 4, 23.798445, 120.642034),
New clsStation(2703, "濁水", "Jhuoshuei", 3, 23.834666, 120.70472),
New clsStation(2704, "龍泉", "Lungcyuan", 4, 23.835188, 120.750404),
New clsStation(2705, "集集", "Jiji", 5, 23.826451, 120.784891),
New clsStation(2706, "水里", "Shueili", 3, 23.818456, 120.853323),
New clsStation(2707, "車埕", "Checheng", 3, 23.832637, 120.865745),
New clsStation(2802, "南調", "Nandiao", 3, 24.081666, 120.538539),
New clsStation(2902, "高雄港", "Kaohsiung Port", 3, 22.63962, 120.302111),
New clsStation(3102, "前鎮", "na", -1, 22.631284, 120.357683),
New clsStation(3202, "花蓮港", "hualien Port", -1, 0, 0),
New clsStation(3302, "中興一號", "na", -1, 24.694166, 121.775256),
New clsStation(3402, "中興二號", "na", -1, 23.759681, 120.614987),
New clsStation(3902, "機廠", "na", -1, 25.063, 121.551996),
New clsStation(4102, "樹調", "ShuDiao", 3, 23.69781, 120.960515),
New clsStation(4202, "東港支線", "na", -1, 23.69781, 120.960515),
New clsStation(4302, "東南支線", "na", -1, 23.69781, 120.960515),
New clsStation(1244, "南科", "Nanke", 3, 23.107024, 120.301924),
New clsStation(5101, "長榮大學", "Chang Jung Christian University", 3, 22.90715, 120.272203),
New clsStation(5102, "沙崙", "Shalun", 3, 22.923848, 120.286403),
New clsStation(1033, "北湖", "BaiHwu", 3, 24.922226, 121.055736),
New clsStation(6103, "海科館", "na", 3, 25.137638, 121.80001),
New clsStation(1243, "仁德", "Rende", 3, 22.9234, 120.240447)
  }


    Public Shared gGoBackLastState As Boolean = True
    Public Shared gLastStartSt As clsStation = gStList.Item(10)
    Public Shared gLastEndSt As clsStation = gStList.Item(50)

    Public Shared gStGrpList As List(Of clsStGrp) = New List(Of clsStGrp) From {New clsStGrp(0, "臺北區", "Taipei", StGrpColor, New List(Of String) From {"福隆", "貢寮", "雙溪", "牡丹", "三貂嶺", "侯硐", "瑞芳", "四腳亭", "暖暖", "基隆", "三坑", "八堵", "七堵", "百福", "五堵", "汐止", "汐科", "南港", "松山", "臺北", "萬華", "板橋", "浮洲", "樹林", "山佳", "鶯歌", "菁桐", "平溪", "嶺腳", "望古", "十分", "大華"}, "臺北"),
               New clsStGrp(1, "桃園區", "Taipei", StGrpColor, New List(Of String) From {"桃園", "內壢", "中壢", "埔心", "楊梅", "富岡"}, "桃園"),
    New clsStGrp(2, "新竹區", "taipei", StGrpColor, New List(Of String) From {"北湖", "湖口", "新豐", "竹北", "北新竹", "新竹", "香山", "千甲", "新莊", "竹中", "上員", "榮華", "竹東", "橫山", "九讚頭", "合興", "富貴", "內灣"}, "新竹"),
    New clsStGrp(3, "苗栗區", "taipei", StGrpColor, New List(Of String) From {"崎頂", "竹南", "談文", "大山", "後龍", "龍港", "白沙屯", "新埔", "通霄", "苑裡", "造橋", "豐富", "苗栗", "南勢", "銅鑼", "三義"}, "苗栗"),
    New clsStGrp(4, "臺中區", "taipei", StGrpColor, New List(Of String) From {"日南", "大甲", "臺中港", "清水", "沙鹿", "龍井", "大肚", "追分", "泰安", "后里", "豐原", "潭子", "太原", "臺中", "大慶", "烏日", "新烏日", "成功"}, "臺中"),
    New clsStGrp(5, "彰化區", "taipei", StGrpColor, New List(Of String) From {"彰化", "花壇", "大村", "員林", "永靖", "社頭", "田中", "二水"}, "彰化"),
    New clsStGrp(6, "南投區", "taipei", StGrpColor, New List(Of String) From {"源泉", "濁水", "龍泉", "集集", "水里", "車埕"}, "集集"),
    New clsStGrp(7, "雲林區", "taipei", StGrpColor, New List(Of String) From {"林內", "石榴", "斗六", "斗南", "石龜"}, "斗六"),
    New clsStGrp(8, "嘉義區", "taipei", StGrpColor, New List(Of String) From {"大林", "民雄", "嘉北", "嘉義", "水上", "南靖"}, "嘉義"),
    New clsStGrp(9, "臺南區", "taipei", StGrpColor, New List(Of String) From {"仁德", "後壁", "新營", "柳營", "林鳳營", "隆田", "拔林", "善化", "南科", "新市", "永康", "大橋", "臺南", "保安", "中洲", "長榮大學", "沙崙"}, "臺南"),
    New clsStGrp(10, "高雄區", "taipei", StGrpColor, New List(Of String) From {"大湖", "路竹", "岡山", "橋頭", "楠梓", "新左營", "左營", "高雄", "鳳山", "後庄", "九曲堂"}, "高雄"),
    New clsStGrp(11, "屏東區", "taipei", StGrpColor, New List(Of String) From {"屏東", "歸來", "麟洛", "西勢", "竹田", "潮州", "崁頂", "南州", "鎮安", "林邊", "佳冬", "東海", "枋寮", "加祿", "內獅", "枋山"}, "屏東"),
    New clsStGrp(12, "臺東區", "taipei", StGrpColor, New List(Of String) From {"古莊", "大武", "瀧溪", "金崙", "太麻里", "知本", "康樂", "臺東", "山里", "鹿野", "瑞源", "瑞和", "關山", "海端", "池上"}, "臺東"),
    New clsStGrp(13, "花蓮區", "taipei", StGrpColor, New List(Of String) From {"富里", "東竹", "東里", "玉里", "三民", "瑞穗", "富源", "大富", "光復", "萬榮", "鳳林", "南平", "豐田", "壽豐", "平和", "志學", "吉安", "花蓮", "北埔", "景美", "新城", "崇德", "和仁", "和平"}, "花蓮"),
    New clsStGrp(14, "宜蘭區", "taipei", StGrpColor, New List(Of String) From {"漢本", "武塔", "南澳", "東澳", "永樂", "蘇澳新站", "新馬", "冬山", "羅東", "中里", "二結", "宜蘭", "四城", "礁溪", "頂埔", "頭城", "外澳", "龜山", "大溪", "大里", "石城"}, "宜蘭"),
    New clsStGrp(15, "平溪線", "taipei", StGrpColor, New List(Of String) From {"菁桐", "平溪", "嶺腳", "望古", "十分", "大華", "三貂嶺"}, "平溪"),
    New clsStGrp(16, "內灣 六家", "taipei", StGrpColor, New List(Of String) From {"新竹", "北新竹", "千甲", "新莊", "竹中", "上員", "榮華", "竹東", "橫山", "九讚頭", "合興", "富貴", "內灣"}, "千甲"),
    New clsStGrp(17, "集集區", "taipei", StGrpColor, New List(Of String) From {"二水", "源泉", "濁水", "龍泉", "集集", "水里", "車埕"}, "集集"),
    New clsStGrp(18, "沙崙區", "taipei", StGrpColor, New List(Of String) From {"長榮大學", "沙崙"}, "沙崙"),
    New clsStGrp(19, "深澳線", "taipei", StGrpColor, New List(Of String) From {"瑞芳", "海科館"}, "瑞芳")
     }

    Public Shared gStGrpList_S As List(Of clsStGrp_S) = New List(Of clsStGrp_S) From {
        New clsStGrp_S("臺北區", "Taipei", StGrpColor),
New clsStGrp_S("桃園區", "Taipei", StGrpColor),
New clsStGrp_S("新竹區", "taipei", StGrpColor),
New clsStGrp_S("苗栗區", "taipei", StGrpColor),
New clsStGrp_S("臺中區", "taipei", StGrpColor),
New clsStGrp_S("彰化區", "taipei", StGrpColor),
New clsStGrp_S("南投區", "taipei", StGrpColor),
New clsStGrp_S("雲林區", "taipei", StGrpColor),
New clsStGrp_S("嘉義區", "taipei", StGrpColor),
New clsStGrp_S("臺南區", "taipei", StGrpColor),
New clsStGrp_S("高雄區", "taipei", StGrpColor),
New clsStGrp_S("屏東區", "taipei", StGrpColor),
New clsStGrp_S("臺東區", "taipei", StGrpColor),
New clsStGrp_S("花蓮區", "taipei", StGrpColor),
New clsStGrp_S("宜蘭區", "taipei", StGrpColor),
New clsStGrp_S("平溪線", "taipei", StGrpColor),
New clsStGrp_S("內灣 六家", "taipei", StGrpColor),
New clsStGrp_S("集集區", "taipei", StGrpColor),
New clsStGrp_S("沙崙區", "taipei", StGrpColor),
New clsStGrp_S("深澳線", "taipei", StGrpColor)
}

    Public Shared gLoc_W As clsLoc_Line = New clsLoc_Line("西部幹線", New List(Of clsLoc_St) From {
New clsLoc_St("基隆", 0), _
New clsLoc_St("三坑", 1.3), _
New clsLoc_St("八堵", 3.7), _
New clsLoc_St("七堵", 6), _
New clsLoc_St("百福", 8.7), _
New clsLoc_St("五堵", 11.7), _
New clsLoc_St("汐止", 13.1), _
New clsLoc_St("汐科", 14.6), _
New clsLoc_St("南港", 19.1), _
New clsLoc_St("松山", 21.9), _
New clsLoc_St("臺北", 28.3), _
New clsLoc_St("萬華", 31.1), _
New clsLoc_St("板橋", 35.5), _
New clsLoc_St("浮洲", 38), _
New clsLoc_St("樹林", 40.9), _
New clsLoc_St("山佳", 44.8), _
New clsLoc_St("鶯歌", 49.2), _
New clsLoc_St("桃園", 57.4), _
New clsLoc_St("內壢", 63.3), _
New clsLoc_St("中壢", 67.3), _
New clsLoc_St("埔心", 73.1), _
New clsLoc_St("楊梅", 77.1), _
New clsLoc_St("富岡", 83.9), _
New clsLoc_St("北湖", 87.1), _
New clsLoc_St("湖口", 89.6), _
New clsLoc_St("新豐", 95.8), _
New clsLoc_St("竹北", 100.6), _
New clsLoc_St("北新竹", 105), _
New clsLoc_St("新竹", 106.4), _
New clsLoc_St("香山", 114.4), _
New clsLoc_St("崎頂", 120.8), _
New clsLoc_St("竹南", 125.4), _
New clsLoc_St("造橋", 130.7), _
New clsLoc_St("豐富", 137.1), _
New clsLoc_St("苗栗", 140.6), _
New clsLoc_St("南勢", 147.2), _
New clsLoc_St("銅鑼", 151.4), _
New clsLoc_St("三義", 158.8), _
New clsLoc_St("泰安", 169.7), _
New clsLoc_St("后里", 172.3), _
New clsLoc_St("豐原", 179.1), _
New clsLoc_St("潭子", 184.1), _
New clsLoc_St("太原", 189.2), _
New clsLoc_St("臺中", 193.3), _
New clsLoc_St("大慶", 197.5), _
New clsLoc_St("烏日", 200.5), _
New clsLoc_St("新烏日", 201.3), _
New clsLoc_St("成功", 203.8), _
New clsLoc_St("彰化", 210.9), _
New clsLoc_St("花壇", 217.5), _
New clsLoc_St("大村", 222.1), _
New clsLoc_St("員林", 225.6), _
New clsLoc_St("永靖", 229.1), _
New clsLoc_St("社頭", 232.8), _
New clsLoc_St("田中", 237.1), _
New clsLoc_St("二水", 242.9), _
New clsLoc_St("林內", 251), _
New clsLoc_St("石榴", 255.8), _
New clsLoc_St("斗六", 260.6), _
New clsLoc_St("斗南", 268.2), _
New clsLoc_St("石龜", 272.1), _
New clsLoc_St("大林", 276.7), _
New clsLoc_St("民雄", 282.5), _
New clsLoc_St("嘉北", 289.2), _
New clsLoc_St("嘉義", 291.8), _
New clsLoc_St("水上", 298.4), _
New clsLoc_St("南靖", 301), _
New clsLoc_St("後壁", 307), _
New clsLoc_St("新營", 314.7), _
New clsLoc_St("柳營", 318), _
New clsLoc_St("林鳳營", 321.9), _
New clsLoc_St("隆田", 327.4), _
New clsLoc_St("拔林", 329.6), _
New clsLoc_St("善化", 334.2), _
New clsLoc_St("南科", 337.1), _
New clsLoc_St("新市", 341.8), _
New clsLoc_St("永康", 346.8), _
New clsLoc_St("大橋", 350.5), _
New clsLoc_St("臺南", 353.2), _
New clsLoc_St("保安", 360.8), _
New clsLoc_St("仁德", 362.2), _
New clsLoc_St("中洲", 364.8), _
New clsLoc_St("大湖", 367.7), _
New clsLoc_St("路竹", 370.6), _
New clsLoc_St("岡山", 378.4), _
New clsLoc_St("橋頭", 382), _
New clsLoc_St("楠梓", 386.2), _
New clsLoc_St("新左營", 391.3), _
New clsLoc_St("左營", 393.2), _
New clsLoc_St("高雄", 399.8), _
New clsLoc_St("鳳山", 405.6), _
New clsLoc_St("後庄", 409.3), _
New clsLoc_St("九曲堂", 413.6), _
New clsLoc_St("六塊厝", 418.6), _
New clsLoc_St("屏東", 420.8) _
})

    Public Shared gLoc_WS As clsLoc_Line = New clsLoc_Line("西部幹線海", New List(Of clsLoc_St) From {
New clsLoc_St("竹南", 0), _
New clsLoc_St("談文", 4.5), _
New clsLoc_St("大山", 11.2), _
New clsLoc_St("後龍", 15), _
New clsLoc_St("龍港", 18.6), _
New clsLoc_St("白沙屯", 26.7), _
New clsLoc_St("新埔", 29.8), _
New clsLoc_St("通霄", 35.6), _
New clsLoc_St("苑裡", 41.7), _
New clsLoc_St("日南", 49.4), _
New clsLoc_St("大甲", 54), _
New clsLoc_St("臺中港", 59.3), _
New clsLoc_St("清水", 65.3), _
New clsLoc_St("沙鹿", 68.5), _
New clsLoc_St("龍井", 73.1), _
New clsLoc_St("大肚", 78.1), _
New clsLoc_St("追分", 83.1), _
New clsLoc_St("彰化", 90.2) _
})

    Public Shared gLoc_E As clsLoc_Line = New clsLoc_Line("東部幹線", New List(Of clsLoc_St) From {
New clsLoc_St("八堵", 0), _
New clsLoc_St("暖暖", 1.6), _
New clsLoc_St("四腳亭", 3.9), _
New clsLoc_St("瑞芳", 8.9), _
New clsLoc_St("侯硐", 13.5), _
New clsLoc_St("三貂嶺", 16), _
New clsLoc_St("牡丹", 19.6), _
New clsLoc_St("雙溪", 22.9), _
New clsLoc_St("貢寮", 28.3), _
New clsLoc_St("福隆", 32), _
New clsLoc_St("石城", 37.4), _
New clsLoc_St("大里", 40.1), _
New clsLoc_St("大溪", 44.8), _
New clsLoc_St("龜山", 49.4), _
New clsLoc_St("外澳", 53), _
New clsLoc_St("頭城", 56.6), _
New clsLoc_St("頂埔", 58.8), _
New clsLoc_St("礁溪", 62.9), _
New clsLoc_St("四城", 67.6), _
New clsLoc_St("宜蘭", 71.3), _
New clsLoc_St("二結", 77.1), _
New clsLoc_St("中里", 78.3), _
New clsLoc_St("羅東", 80.1), _
New clsLoc_St("冬山", 85.1), _
New clsLoc_St("新馬", 89.3), _
New clsLoc_St("蘇澳新站", 90.2), _
New clsLoc_St("永樂", 95.4), _
New clsLoc_St("東澳", 101.2), _
New clsLoc_St("南澳", 109.2), _
New clsLoc_St("武塔", 112.9), _
New clsLoc_St("漢本", 125.8), _
New clsLoc_St("和平", 130), _
New clsLoc_St("和仁", 137.7), _
New clsLoc_St("崇德", 147.8), _
New clsLoc_St("新城", 153.1), _
New clsLoc_St("景美", 158.4), _
New clsLoc_St("北埔", 164.9), _
New clsLoc_St("花蓮", 169.4), _
New clsLoc_St("吉安", 172.8), _
New clsLoc_St("志學", 181.8), _
New clsLoc_St("平和", 184.7), _
New clsLoc_St("壽豐", 186.6), _
New clsLoc_St("豐田", 189.3), _
New clsLoc_St("南平", 197.8), _
New clsLoc_St("鳳林", 201.9), _
New clsLoc_St("萬榮", 206.7), _
New clsLoc_St("光復", 212.3), _
New clsLoc_St("大富", 220), _
New clsLoc_St("富源", 223), _
New clsLoc_St("瑞穗", 232.3), _
New clsLoc_St("三民", 241.5), _
New clsLoc_St("玉里", 253), _
New clsLoc_St("東里", 259.7), _
New clsLoc_St("東竹", 265.6), _
New clsLoc_St("富里", 271.8), _
New clsLoc_St("池上", 278.8), _
New clsLoc_St("海端", 284.5), _
New clsLoc_St("關山", 290.9), _
New clsLoc_St("瑞和", 298.8), _
New clsLoc_St("瑞源", 301.5), _
New clsLoc_St("鹿野", 307.1), _
New clsLoc_St("山里", 313.3), _
New clsLoc_St("臺東", 321.3) _
})

    Public Shared gLoc_SN As clsLoc_Line = New clsLoc_Line("蘇澳新", New List(Of clsLoc_St) From {
New clsLoc_St("蘇澳新站", 0), _
New clsLoc_St("蘇澳", 3.4)
})

    Public Shared gLoc_P As clsLoc_Line = New clsLoc_Line("屏東南迴", New List(Of clsLoc_St) From {
New clsLoc_St("屏東", 0), _
New clsLoc_St("歸來", 2.6), _
New clsLoc_St("麟洛", 4.9), _
New clsLoc_St("西勢", 7.3), _
New clsLoc_St("竹田", 11), _
New clsLoc_St("潮州", 15.1), _
New clsLoc_St("崁頂", 19.9), _
New clsLoc_St("南州", 22.3), _
New clsLoc_St("鎮安", 25.9), _
New clsLoc_St("林邊", 29.1), _
New clsLoc_St("佳冬", 33.1), _
New clsLoc_St("東海", 36.2), _
New clsLoc_St("枋寮", 40.3), _
New clsLoc_St("加祿", 45.6), _
New clsLoc_St("內獅", 49), _
New clsLoc_St("枋山", 53.9), _
New clsLoc_St("古莊", 80.8), _
New clsLoc_St("大武", 84.1), _
New clsLoc_St("瀧溪", 95.8), _
New clsLoc_St("金崙", 104.2), _
New clsLoc_St("太麻里", 115.2), _
New clsLoc_St("知本", 126.9), _
New clsLoc_St("康樂", 133.9), _
New clsLoc_St("臺東", 138.5)
})


    Public Shared gLoc_C As clsLoc_Line = New clsLoc_Line("平溪線", New List(Of clsLoc_St) From {
New clsLoc_St("三貂嶺", 0), _
New clsLoc_St("大華", 3.5), _
New clsLoc_St("十分", 6.4), _
New clsLoc_St("望古", 8.2), _
New clsLoc_St("嶺腳", 10.2), _
New clsLoc_St("平溪", 11.2), _
New clsLoc_St("菁桐", 12.9)
   })

    Public Shared gLoc_NW As clsLoc_Line = New clsLoc_Line("內灣線", New List(Of clsLoc_St) From {
New clsLoc_St("新竹", 0), _
New clsLoc_St("北新竹", 1.4), _
New clsLoc_St("千甲", 3.6), _
New clsLoc_St("新莊", 6.6), _
New clsLoc_St("竹中", 7.9), _
New clsLoc_St("上員", 10.5), _
New clsLoc_St("六家", 11), _
New clsLoc_St("榮華", 15), _
New clsLoc_St("竹東", 16.6), _
New clsLoc_St("橫山", 20), _
New clsLoc_St("九讚頭", 22.2), _
New clsLoc_St("合興", 24.4), _
New clsLoc_St("富貴", 25.7), _
New clsLoc_St("內灣", 27.9)
   })

    Public Shared gLoc_GG As clsLoc_Line = New clsLoc_Line("集集線", New List(Of clsLoc_St) From {
New clsLoc_St("二水", 0), _
New clsLoc_St("源泉", 2.9), _
New clsLoc_St("濁水", 10.8), _
New clsLoc_St("龍泉", 15.7), _
New clsLoc_St("集集", 20.1), _
New clsLoc_St("水里", 27.4), _
New clsLoc_St("車埕", 29.7)
   })

    Public Shared gLoc_CG As clsLoc_Line = New clsLoc_Line("成追線", New List(Of clsLoc_St) From {
New clsLoc_St("成功", 0), _
New clsLoc_St("追分", 2.2)
   })

    Public Shared gLoc_SL As clsLoc_Line = New clsLoc_Line("沙崙線", New List(Of clsLoc_St) From {
New clsLoc_St("中洲", 0), _
New clsLoc_St("長榮大學", 2.6), _
New clsLoc_St("沙崙", 5.3)
   })
    Public Shared gLoc_SR As clsLoc_Line = New clsLoc_Line("深澳線", New List(Of clsLoc_St) From {
New clsLoc_St("瑞芳", 0), _
New clsLoc_St("海科館", 4.2)
   })

    ''山線總長 876.9
    Public Shared gLoc_CirMountain As clsLoc_Line = New clsLoc_Line("山線環台", New List(Of clsLoc_St) From {
    New clsLoc_St("八堵", 0), _
New clsLoc_St("七堵", 2.3), _
New clsLoc_St("百福", 5), _
New clsLoc_St("五堵", 8), _
New clsLoc_St("汐止", 9.4), _
New clsLoc_St("汐科", 10.9), _
New clsLoc_St("南港", 15.4), _
New clsLoc_St("松山", 18.2), _
New clsLoc_St("臺北", 24.6), _
New clsLoc_St("萬華", 27.4), _
New clsLoc_St("板橋", 31.8), _
New clsLoc_St("浮洲", 34.3), _
New clsLoc_St("樹林", 37.2), _
New clsLoc_St("山佳", 41.1), _
New clsLoc_St("鶯歌", 45.5), _
New clsLoc_St("桃園", 53.7), _
New clsLoc_St("內壢", 59.6), _
New clsLoc_St("中壢", 63.6), _
New clsLoc_St("埔心", 69.4), _
New clsLoc_St("楊梅", 73.4), _
New clsLoc_St("富岡", 80.2), _
New clsLoc_St("北湖", 83.4), _
New clsLoc_St("湖口", 85.9), _
New clsLoc_St("新豐", 92.1), _
New clsLoc_St("竹北", 96.9), _
New clsLoc_St("北新竹", 101.3), _
New clsLoc_St("新竹", 102.7), _
New clsLoc_St("香山", 110.7), _
New clsLoc_St("崎頂", 117.1), _
New clsLoc_St("竹南", 121.7), _
New clsLoc_St("造橋", 127), _
New clsLoc_St("豐富", 133.4), _
New clsLoc_St("苗栗", 136.9), _
New clsLoc_St("南勢", 143.5), _
New clsLoc_St("銅鑼", 147.7), _
New clsLoc_St("三義", 155.1), _
New clsLoc_St("泰安", 166), _
New clsLoc_St("后里", 168.6), _
New clsLoc_St("豐原", 175.4), _
New clsLoc_St("潭子", 180.4), _
New clsLoc_St("太原", 185.5), _
New clsLoc_St("臺中", 189.6), _
New clsLoc_St("大慶", 193.8), _
New clsLoc_St("烏日", 196.8), _
New clsLoc_St("新烏日", 197.6), _
New clsLoc_St("成功", 200.1), _
New clsLoc_St("彰化", 207.2), _
New clsLoc_St("花壇", 213.8), _
New clsLoc_St("大村", 218.4), _
New clsLoc_St("員林", 221.9), _
New clsLoc_St("永靖", 225.4), _
New clsLoc_St("社頭", 229.1), _
New clsLoc_St("田中", 233.4), _
New clsLoc_St("二水", 239.2), _
New clsLoc_St("林內", 247.3), _
New clsLoc_St("石榴", 252.1), _
New clsLoc_St("斗六", 256.9), _
New clsLoc_St("斗南", 264.5), _
New clsLoc_St("石龜", 268.4), _
New clsLoc_St("大林", 273), _
New clsLoc_St("民雄", 278.8), _
New clsLoc_St("嘉北", 285.5), _
New clsLoc_St("嘉義", 288.1), _
New clsLoc_St("水上", 294.7), _
New clsLoc_St("南靖", 297.3), _
New clsLoc_St("後壁", 303.3), _
New clsLoc_St("新營", 311), _
New clsLoc_St("柳營", 314.3), _
New clsLoc_St("林鳳營", 318.2), _
New clsLoc_St("隆田", 323.7), _
New clsLoc_St("拔林", 325.9), _
New clsLoc_St("善化", 330.5), _
New clsLoc_St("南科", 333.4), _
New clsLoc_St("新市", 338.1), _
New clsLoc_St("永康", 343.1), _
New clsLoc_St("大橋", 346.8), _
New clsLoc_St("臺南", 349.5), _
New clsLoc_St("保安", 357.1), _
New clsLoc_St("仁德", 358.5), _
New clsLoc_St("中洲", 361.1), _
New clsLoc_St("大湖", 364), _
New clsLoc_St("路竹", 366.9), _
New clsLoc_St("岡山", 374.7), _
New clsLoc_St("橋頭", 378.3), _
New clsLoc_St("楠梓", 382.5), _
New clsLoc_St("新左營", 387.6), _
New clsLoc_St("左營", 389.5), _
New clsLoc_St("高雄", 396.1), _
New clsLoc_St("鳳山", 401.9), _
New clsLoc_St("後庄", 405.6), _
New clsLoc_St("九曲堂", 409.9), _
New clsLoc_St("六塊厝", 414.9), _
New clsLoc_St("屏東", 417.1), _
New clsLoc_St("歸來", 419.7), _
New clsLoc_St("麟洛", 422), _
New clsLoc_St("西勢", 424.4), _
New clsLoc_St("竹田", 428.1), _
New clsLoc_St("潮州", 432.2), _
New clsLoc_St("崁頂", 437), _
New clsLoc_St("南州", 439.4), _
New clsLoc_St("鎮安", 443), _
New clsLoc_St("林邊", 446.2), _
New clsLoc_St("佳冬", 450.2), _
New clsLoc_St("東海", 453.3), _
New clsLoc_St("枋寮", 457.4), _
New clsLoc_St("加祿", 462.7), _
New clsLoc_St("內獅", 466.1), _
New clsLoc_St("枋山", 471), _
New clsLoc_St("古莊", 497.9), _
New clsLoc_St("大武", 501.2), _
New clsLoc_St("瀧溪", 512.9), _
New clsLoc_St("金崙", 521.3), _
New clsLoc_St("太麻里", 532.3), _
New clsLoc_St("知本", 544), _
New clsLoc_St("康樂", 551), _
New clsLoc_St("臺東", 555.6), _
New clsLoc_St("臺東", 555.6), _
New clsLoc_St("山里", 563.6), _
New clsLoc_St("鹿野", 569.8), _
New clsLoc_St("瑞源", 575.4), _
New clsLoc_St("瑞和", 578.1), _
New clsLoc_St("關山", 586), _
New clsLoc_St("海端", 592.4), _
New clsLoc_St("池上", 598.1), _
New clsLoc_St("富里", 605.1), _
New clsLoc_St("東竹", 611.3), _
New clsLoc_St("東里", 617.2), _
New clsLoc_St("玉里", 623.9), _
New clsLoc_St("三民", 635.4), _
New clsLoc_St("瑞穗", 644.6), _
New clsLoc_St("富源", 653.9), _
New clsLoc_St("大富", 656.9), _
New clsLoc_St("光復", 664.6), _
New clsLoc_St("萬榮", 670.2), _
New clsLoc_St("鳳林", 675), _
New clsLoc_St("南平", 679.1), _
New clsLoc_St("豐田", 687.6), _
New clsLoc_St("壽豐", 690.3), _
New clsLoc_St("平和", 692.2), _
New clsLoc_St("志學", 695.1), _
New clsLoc_St("吉安", 704.1), _
New clsLoc_St("花蓮", 707.5), _
New clsLoc_St("北埔", 712), _
New clsLoc_St("景美", 718.5), _
New clsLoc_St("新城", 723.8), _
New clsLoc_St("崇德", 729.1), _
New clsLoc_St("和仁", 739.2), _
New clsLoc_St("和平", 746.9), _
New clsLoc_St("漢本", 751.1), _
New clsLoc_St("武塔", 764), _
New clsLoc_St("南澳", 767.7), _
New clsLoc_St("東澳", 775.7), _
New clsLoc_St("永樂", 781.5), _
New clsLoc_St("蘇澳新站", 786.7), _
New clsLoc_St("新馬", 787.6), _
New clsLoc_St("冬山", 791.8), _
New clsLoc_St("羅東", 796.8), _
New clsLoc_St("中里", 798.6), _
New clsLoc_St("二結", 799.8), _
New clsLoc_St("宜蘭", 805.6), _
New clsLoc_St("四城", 809.3), _
New clsLoc_St("礁溪", 814), _
New clsLoc_St("頂埔", 818.1), _
New clsLoc_St("頭城", 820.3), _
New clsLoc_St("外澳", 823.9), _
New clsLoc_St("龜山", 827.5), _
New clsLoc_St("大溪", 832.1), _
New clsLoc_St("大里", 836.8), _
New clsLoc_St("石城", 839.5), _
New clsLoc_St("福隆", 844.9), _
New clsLoc_St("貢寮", 848.6), _
New clsLoc_St("雙溪", 854), _
New clsLoc_St("牡丹", 857.3), _
New clsLoc_St("三貂嶺", 860.9), _
New clsLoc_St("侯硐", 863.4), _
New clsLoc_St("瑞芳", 868), _
New clsLoc_St("四腳亭", 873), _
New clsLoc_St("暖暖", 875.3)
   })

    ''海線總長 881.6

    Public Shared gLoc_CirSea As clsLoc_Line = New clsLoc_Line("海線環台", New List(Of clsLoc_St) From {
New clsLoc_St("八堵", 0), _
New clsLoc_St("七堵", 2.3), _
New clsLoc_St("百福", 5), _
New clsLoc_St("五堵", 8), _
New clsLoc_St("汐止", 9.4), _
New clsLoc_St("汐科", 10.9), _
New clsLoc_St("南港", 15.4), _
New clsLoc_St("松山", 18.2), _
New clsLoc_St("臺北", 24.6), _
New clsLoc_St("萬華", 27.4), _
New clsLoc_St("板橋", 31.8), _
New clsLoc_St("浮洲", 34.3), _
New clsLoc_St("樹林", 37.2), _
New clsLoc_St("山佳", 41.1), _
New clsLoc_St("鶯歌", 45.5), _
New clsLoc_St("桃園", 53.7), _
New clsLoc_St("內壢", 59.6), _
New clsLoc_St("中壢", 63.6), _
New clsLoc_St("埔心", 69.4), _
New clsLoc_St("楊梅", 73.4), _
New clsLoc_St("富岡", 80.2), _
New clsLoc_St("北湖", 83.4), _
New clsLoc_St("湖口", 85.9), _
New clsLoc_St("新豐", 92.1), _
New clsLoc_St("竹北", 96.9), _
New clsLoc_St("北新竹", 101.3), _
New clsLoc_St("新竹", 102.7), _
New clsLoc_St("香山", 110.7), _
New clsLoc_St("崎頂", 117.1), _
New clsLoc_St("竹南", 121.7), _
New clsLoc_St("談文", 126.2), _
New clsLoc_St("大山", 132.9), _
New clsLoc_St("後龍", 136.7), _
New clsLoc_St("龍港", 140.3), _
New clsLoc_St("白沙屯", 148.4), _
New clsLoc_St("新埔", 151.5), _
New clsLoc_St("通霄", 157.3), _
New clsLoc_St("苑裡", 163.4), _
New clsLoc_St("日南", 171.1), _
New clsLoc_St("大甲", 175.7), _
New clsLoc_St("臺中港", 181), _
New clsLoc_St("清水", 187), _
New clsLoc_St("沙鹿", 190.2), _
New clsLoc_St("龍井", 194.8), _
New clsLoc_St("大肚", 199.8), _
New clsLoc_St("追分", 204.8), _
New clsLoc_St("彰化", 211.9), _
New clsLoc_St("花壇", 218.5), _
New clsLoc_St("大村", 223.1), _
New clsLoc_St("員林", 226.6), _
New clsLoc_St("永靖", 230.1), _
New clsLoc_St("社頭", 233.8), _
New clsLoc_St("田中", 238.1), _
New clsLoc_St("二水", 243.9), _
New clsLoc_St("林內", 252), _
New clsLoc_St("石榴", 256.8), _
New clsLoc_St("斗六", 261.6), _
New clsLoc_St("斗南", 269.2), _
New clsLoc_St("石龜", 273.1), _
New clsLoc_St("大林", 277.7), _
New clsLoc_St("民雄", 283.5), _
New clsLoc_St("嘉北", 290.2), _
New clsLoc_St("嘉義", 292.8), _
New clsLoc_St("水上", 299.4), _
New clsLoc_St("南靖", 302), _
New clsLoc_St("後壁", 308), _
New clsLoc_St("新營", 315.7), _
New clsLoc_St("柳營", 319), _
New clsLoc_St("林鳳營", 322.9), _
New clsLoc_St("隆田", 328.4), _
New clsLoc_St("拔林", 330.6), _
New clsLoc_St("善化", 335.2), _
New clsLoc_St("南科", 338.1), _
New clsLoc_St("新市", 342.8), _
New clsLoc_St("永康", 347.8), _
New clsLoc_St("大橋", 351.5), _
New clsLoc_St("臺南", 354.2), _
New clsLoc_St("保安", 361.8), _
New clsLoc_St("仁德", 363.2), _
New clsLoc_St("中洲", 365.8), _
New clsLoc_St("大湖", 368.7), _
New clsLoc_St("路竹", 371.6), _
New clsLoc_St("岡山", 379.4), _
New clsLoc_St("橋頭", 383), _
New clsLoc_St("楠梓", 387.2), _
New clsLoc_St("新左營", 392.3), _
New clsLoc_St("左營", 394.2), _
New clsLoc_St("高雄", 400.8), _
New clsLoc_St("鳳山", 406.6), _
New clsLoc_St("後庄", 410.3), _
New clsLoc_St("九曲堂", 414.6), _
New clsLoc_St("六塊厝", 419.6), _
New clsLoc_St("屏東", 421.8), _
New clsLoc_St("歸來", 424.4), _
New clsLoc_St("麟洛", 426.7), _
New clsLoc_St("西勢", 429.1), _
New clsLoc_St("竹田", 432.8), _
New clsLoc_St("潮州", 436.9), _
New clsLoc_St("崁頂", 441.7), _
New clsLoc_St("南州", 444.1), _
New clsLoc_St("鎮安", 447.7), _
New clsLoc_St("林邊", 450.9), _
New clsLoc_St("佳冬", 454.9), _
New clsLoc_St("東海", 458), _
New clsLoc_St("枋寮", 462.1), _
New clsLoc_St("加祿", 467.4), _
New clsLoc_St("內獅", 470.8), _
New clsLoc_St("枋山", 475.7), _
New clsLoc_St("古莊", 502.6), _
New clsLoc_St("大武", 505.9), _
New clsLoc_St("瀧溪", 517.6), _
New clsLoc_St("金崙", 526), _
New clsLoc_St("太麻里", 537), _
New clsLoc_St("知本", 548.7), _
New clsLoc_St("康樂", 555.7), _
New clsLoc_St("臺東", 560.3), _
New clsLoc_St("臺東", 560.3), _
New clsLoc_St("山里", 568.3), _
New clsLoc_St("鹿野", 574.5), _
New clsLoc_St("瑞源", 580.1), _
New clsLoc_St("瑞和", 582.8), _
New clsLoc_St("關山", 590.7), _
New clsLoc_St("海端", 597.1), _
New clsLoc_St("池上", 602.8), _
New clsLoc_St("富里", 609.8), _
New clsLoc_St("東竹", 616), _
New clsLoc_St("東里", 621.9), _
New clsLoc_St("玉里", 628.6), _
New clsLoc_St("三民", 640.1), _
New clsLoc_St("瑞穗", 649.3), _
New clsLoc_St("富源", 658.6), _
New clsLoc_St("大富", 661.6), _
New clsLoc_St("光復", 669.3), _
New clsLoc_St("萬榮", 674.9), _
New clsLoc_St("鳳林", 679.7), _
New clsLoc_St("南平", 683.8), _
New clsLoc_St("豐田", 692.3), _
New clsLoc_St("壽豐", 695), _
New clsLoc_St("平和", 696.9), _
New clsLoc_St("志學", 699.8), _
New clsLoc_St("吉安", 708.8), _
New clsLoc_St("花蓮", 712.2), _
New clsLoc_St("北埔", 716.7), _
New clsLoc_St("景美", 723.2), _
New clsLoc_St("新城", 728.5), _
New clsLoc_St("崇德", 733.8), _
New clsLoc_St("和仁", 743.9), _
New clsLoc_St("和平", 751.6), _
New clsLoc_St("漢本", 755.8), _
New clsLoc_St("武塔", 768.7), _
New clsLoc_St("南澳", 772.4), _
New clsLoc_St("東澳", 780.4), _
New clsLoc_St("永樂", 786.2), _
New clsLoc_St("蘇澳新站", 791.4), _
New clsLoc_St("新馬", 792.3), _
New clsLoc_St("冬山", 796.5), _
New clsLoc_St("羅東", 801.5), _
New clsLoc_St("中里", 803.3), _
New clsLoc_St("二結", 804.5), _
New clsLoc_St("宜蘭", 810.3), _
New clsLoc_St("四城", 814), _
New clsLoc_St("礁溪", 818.7), _
New clsLoc_St("頂埔", 822.8), _
New clsLoc_St("頭城", 825), _
New clsLoc_St("外澳", 828.6), _
New clsLoc_St("龜山", 832.2), _
New clsLoc_St("大溪", 836.8), _
New clsLoc_St("大里", 841.5), _
New clsLoc_St("石城", 844.2), _
New clsLoc_St("福隆", 849.6), _
New clsLoc_St("貢寮", 853.3), _
New clsLoc_St("雙溪", 858.7), _
New clsLoc_St("牡丹", 862), _
New clsLoc_St("三貂嶺", 865.6), _
New clsLoc_St("侯硐", 868.1), _
New clsLoc_St("瑞芳", 872.7), _
New clsLoc_St("四腳亭", 877.7), _
New clsLoc_St("暖暖", 880)
  })
    Public Shared gLocLineList As List(Of clsLoc_Line) = New List(Of clsLoc_Line) From {gLoc_W, gLoc_WS, gLoc_E, gLoc_SN, gLoc_P, gLoc_C, gLoc_NW, gLoc_GG, gLoc_CG, gLoc_SL, gLoc_SR}
End Class
