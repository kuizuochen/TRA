Imports System.Collections.ObjectModel

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


    Public Shared gStList As List(Of clsStation) = New List(Of clsStation) From {
  New clsStation(1001, "基隆", "Keelung", 1),
 New clsStation(1002, "八堵", "Badu", 2),
 New clsStation(1003, "七堵", "Cidu", 1),
 New clsStation(1004, "五堵", "Wudu"),
 New clsStation(1005, "汐止", "Sijhih", 2),
 New clsStation(1006, "南港", "Nangang", 2),
 New clsStation(1007, "松山", "Songshan", 1),
 New clsStation(1008, "臺北", "Taipei", 0),
 New clsStation(1009, "萬華", "Wanhua", 1),
 New clsStation(1011, "板橋", "Banciao", 1),
 New clsStation(1032, "浮洲", "Fuzhou"),
 New clsStation(1012, "樹林", "Shulin", 1),
 New clsStation(1013, "山佳", "Shanjia"),
 New clsStation(1014, "鶯歌", "Yingge", 2),
 New clsStation(1015, "桃園", "Taoyuan", 1),
 New clsStation(1016, "內壢", "Neili"),
 New clsStation(1017, "中壢", "Jhongli", 1),
 New clsStation(1018, "埔心", "Pusin"),
 New clsStation(1019, "楊梅", "Yangmei"),
 New clsStation(1020, "富岡", "Fugang"),
 New clsStation(1021, "湖口", "Hukou"),
 New clsStation(1022, "新豐", "Sinfong"),
 New clsStation(1023, "竹北", "Jhubei"),
 New clsStation(1024, "北新竹", "North Hsinchu"),
 New clsStation(1025, "新竹", "Hsinchu", 1),
 New clsStation(1026, "香山", "Siangshan"),
 New clsStation(1027, "崎頂", "Ciding", 4),
 New clsStation(1028, "竹南", "Jhunan", 1),
 New clsStation(1029, "三坑", "Sankeng"),
 New clsStation(1030, "百福", "na", -1),
 New clsStation(1031, "汐科", "na", -1),
 New clsStation(1102, "談文", "Tanwan", 4),
 New clsStation(1103, "談文南", "Tanwannan", 8),
 New clsStation(1104, "大山", "Dashan"),
 New clsStation(1105, "後龍", "Houlong"),
 New clsStation(1106, "龍港", "Longgang", 4),
 New clsStation(1107, "白沙屯", "Baishatun"),
 New clsStation(1108, "新埔", "Sinpu"),
 New clsStation(1109, "通霄", "Tongsiao"),
 New clsStation(1110, "苑裡", "Yuanli"),
 New clsStation(1111, "日南", "Rihnan"),
 New clsStation(1112, "大甲", "Dajia", 2),
 New clsStation(1113, "臺中港", "Taichung Port", 2),
 New clsStation(1114, "清水", "Cingshuei"),
 New clsStation(1115, "沙鹿", "Shalu", 2),
 New clsStation(1116, "龍井", "Longjing"),
 New clsStation(1117, "大肚", "Dadu"),
 New clsStation(1118, "追分", "Jhuifen"),
 New clsStation(1119, "大肚溪南", "na", 8),
 New clsStation(1120, "彰化", "Changhua", 1),
 New clsStation(1202, "花壇", "Huatan"),
 New clsStation(1203, "員林", "Yuanlin", 1),
 New clsStation(1204, "永靖", "Yongjing", 4),
 New clsStation(1205, "社頭", "Shetou"),
 New clsStation(1206, "田中", "Tianjhong", 2),
 New clsStation(1207, "二水", "Ershuei", 2),
 New clsStation(1208, "林內", "Linnei"),
 New clsStation(1209, "石榴", "Shihliou", 4),
 New clsStation(1210, "斗六", "Douliou", 1),
 New clsStation(1211, "斗南", "Dounan", 2),
 New clsStation(1212, "石龜", "Shihguei", 4),
 New clsStation(1213, "大林", "Dalin"),
 New clsStation(1214, "民雄", "Minsyong"),
 New clsStation(1215, "嘉義", "Chiayi", 1),
 New clsStation(1217, "水上", "Shueishang"),
 New clsStation(1218, "南靖", "Nanjing"),
 New clsStation(1219, "後壁", "Houbi"),
 New clsStation(1220, "新營", "Sinying", 1),
 New clsStation(1221, "柳營", "Liouying"),
 New clsStation(1222, "林鳳營", "Linfongying"),
 New clsStation(1223, "隆田", "Longtian", 2),
 New clsStation(1224, "拔林", "Balin", 4),
 New clsStation(1225, "善化", "Shanhua", 2),
 New clsStation(1226, "新市", "Sinshih"),
 New clsStation(1227, "永康", "Yongkang", 2),
 New clsStation(1228, "臺南", "Tainan", 1),
 New clsStation(1229, "保安", "Baoan"),
 New clsStation(1230, "中洲", "Jhongjhou", 2),
 New clsStation(1231, "大湖", "Dahu"),
 New clsStation(1232, "路竹", "Lujhu"),
 New clsStation(1233, "岡山", "Gangshan", 1),
 New clsStation(1234, "橋頭", "Ciaotou"),
 New clsStation(1235, "楠梓", "Nanzih", 2),
 New clsStation(1236, "左營", "Zuoying"),
 New clsStation(1237, "鼓山", "Gushan"),
 New clsStation(1238, "高雄", "Kaohsiung", 0),
 New clsStation(1239, "大橋", "Daciao"),
 New clsStation(1240, "大村", "Datsun"),
 New clsStation(1241, "嘉北", "ChiaPei"),
 New clsStation(1242, "新左營", "na", 1),
 New clsStation(1302, "造橋", "Zaociao", 4),
 New clsStation(1304, "豐富", "Fongfu", 4),
 New clsStation(1305, "苗栗", "Miaoli", 1),
 New clsStation(1307, "南勢", "Nanshih", 4),
 New clsStation(1308, "銅鑼", "Tongluo"),
 New clsStation(1310, "三義", "Sanyi"),
 New clsStation(1314, "泰安", "Taian"),
 New clsStation(1315, "后里", "Houli"),
 New clsStation(1317, "豐原", "Fongyuan", 1),
 New clsStation(1318, "潭子", "Tanzih"),
 New clsStation(1319, "臺中", "Taichung", 0),
 New clsStation(1320, "烏日", "Wurih"),
 New clsStation(1321, "成功", "Chenggong"),
 New clsStation(1322, "大慶", "Dacing"),
 New clsStation(1323, "太原", "Taiyuan"),
 New clsStation(1324, "新烏日", "na", -1),
 New clsStation(1402, "鳳山", "Fongshan", 2),
 New clsStation(1403, "後庄", "Houjhuang"),
 New clsStation(1404, "九曲堂", "Jioucyutang"),
 New clsStation(1405, "六塊厝", "Lioukuaicuo", 4),
 New clsStation(1406, "屏東", "Pingtung", 1),
 New clsStation(1407, "歸來", "Gueilai", 4),
 New clsStation(1408, "麟洛", "Linluo", 4),
 New clsStation(1409, "西勢", "Sishih", 8),
 New clsStation(1410, "竹田", "Jhutian"),
 New clsStation(1411, "潮州", "Chaojhou"),
 New clsStation(1412, "崁頂", "Kanding", 4),
 New clsStation(1413, "南州", "Nanjhou"),
 New clsStation(1414, "鎮安", "Jhenan", 4),
 New clsStation(1415, "林邊", "Linbian"),
 New clsStation(1416, "佳冬", "Jiadong"),
 New clsStation(1417, "東海", "Donghai", 4),
 New clsStation(1418, "枋寮", "Fangliao"),
 New clsStation(1502, "加祿", "Jialu"),
 New clsStation(1503, "內獅", "Neishih", 4),
 New clsStation(1504, "枋山", "Fangshan", 4),
 New clsStation(1505, "枋野", "Fangye"),
 New clsStation(1506, "中央", "na", -1),
 New clsStation(1507, "古莊", "Gujhuang"),
 New clsStation(1508, "大武", "Dawu"),
 New clsStation(1510, "瀧溪", "Lunghsi"),
 New clsStation(1511, "多良", "na", -1),
 New clsStation(1512, "金崙", "Jinlun"),
 New clsStation(1514, "太麻里", "Taimali"),
 New clsStation(1516, "知本", "Jhihben"),
 New clsStation(1517, "康樂", "Kangle"),
 New clsStation(1602, "吉安", "Jian"),
 New clsStation(1604, "志學", "Jhihsyue"),
 New clsStation(1605, "平和", "Pinghe", 4),
 New clsStation(1606, "壽豐", "Shoufong"),
 New clsStation(1607, "豐田", "Fongtian"),
 New clsStation(1608, "溪口", "Sikou"),
 New clsStation(1609, "南平", "Nanping"),
 New clsStation(1610, "鳳林", "Fonglin"),
 New clsStation(1611, "萬榮", "Wanrong"),
 New clsStation(1612, "光復", "Guangfu"),
 New clsStation(1613, "大富", "Dafu", 4),
 New clsStation(1614, "富源", "Fuyuan"),
 New clsStation(1616, "瑞穗", "Rueisuei"),
 New clsStation(1617, "三民", "Sanmin"),
 New clsStation(1619, "玉里", "Yuli", 1),
 New clsStation(1620, "安通", "Antung"),
 New clsStation(1621, "東里", "Dongli"),
 New clsStation(1622, "東竹", "Dongjhu"),
 New clsStation(1623, "富里", "Fuli"),
 New clsStation(1624, "池上", "Chihshang"),
 New clsStation(1625, "海端", "Haiduan"),
 New clsStation(1626, "關山", "Guanshan"),
 New clsStation(1627, "月美", "Yuemei", -1),
 New clsStation(1628, "瑞和", "Rueihe", 4),
 New clsStation(1629, "瑞源", "Rueiyuan"),
 New clsStation(1630, "鹿野", "Luye"),
 New clsStation(1631, "山里", "Shanli"),
 New clsStation(1632, "臺東", "Taitung", 1),
 New clsStation(1633, "馬(廢)蘭", "na", -1),
 New clsStation(1634, "臺(廢)東", "na", -1),
 New clsStation(1635, "舞鶴", "Wuhe"),
 New clsStation(1703, "永樂", "Yongle"),
 New clsStation(1704, "東澳", "Dongao", 2),
 New clsStation(1705, "南澳", "Nanao"),
 New clsStation(1706, "武塔", "Wuta", 4),
 New clsStation(1708, "漢本", "Hanben"),
 New clsStation(1709, "和平", "Heping", 2),
 New clsStation(1710, "和仁", "Horen"),
 New clsStation(1711, "崇德", "Chongde"),
 New clsStation(1712, "新城", "Sincheng", 2),
 New clsStation(1713, "景美", "Jingmei"),
 New clsStation(1714, "北埔", "Beipu"),
 New clsStation(1715, "花蓮", "Hualien", 0),
 New clsStation(1802, "暖暖", "Nuannuan", 4),
 New clsStation(1803, "四腳亭", "Sihjiaoting"),
 New clsStation(1804, "瑞芳", "Rueifang", 1),
 New clsStation(1805, "侯硐", "Houtung"),
 New clsStation(1806, "三貂嶺", "Sandiaoling"),
 New clsStation(1807, "牡丹", "Mudan"),
 New clsStation(1808, "雙溪", "Shuangsi", 2),
 New clsStation(1809, "貢寮", "Gungliao"),
 New clsStation(1810, "福隆", "Fulong"),
 New clsStation(1811, "石城", "Shihcheng", 4),
 New clsStation(1812, "大里", "Dali"),
 New clsStation(1813, "大溪", "Dasi"),
 New clsStation(1814, "龜山", "Gueishan"),
 New clsStation(1815, "外澳", "Waiao", 4),
 New clsStation(1816, "頭城", "Toucheng"),
 New clsStation(1817, "頂埔", "Dingpu", 4),
 New clsStation(1818, "礁溪", "Jiaohsi"),
 New clsStation(1819, "四城", "Sihcheng"),
 New clsStation(1820, "宜蘭", "Yilan", 1),
 New clsStation(1821, "二結", "Erjie"),
 New clsStation(1822, "中里", "Jhongli", 4),
 New clsStation(1823, "羅東", "Luodong", 2),
 New clsStation(1824, "冬山", "Dongshan", 2),
 New clsStation(1825, "新馬", "Sinma", 4),
 New clsStation(1826, "蘇澳新站", "Suaosin", 2),
 New clsStation(1827, "蘇澳", "Suao", 1),
 New clsStation(1903, "大華", "Dahua", 4),
 New clsStation(1904, "十分", "Shihfen"),
 New clsStation(1905, "望古", "Wanggu", 4),
 New clsStation(1906, "嶺腳", "Lingjiao", 4),
 New clsStation(1907, "平溪", "Pingsi"),
 New clsStation(1908, "菁桐", "Jingtong"),
 New clsStation(2002, "深澳", "na", -1),
 New clsStation(2102, "五福", "na", -1),
 New clsStation(2103, "林口", "Linkou"),
 New clsStation(2104, "電廠", "na", -1),
 New clsStation(2105, "桃中", "na", -1),
 New clsStation(2106, "寶山", "na", -1),
 New clsStation(2107, "南祥", "na", -1),
 New clsStation(2108, "長興", "Hengshan"),
 New clsStation(2109, "海山站", "na", -1),
 New clsStation(2110, "海湖站", "na", -1),
 New clsStation(2212, "千甲", "Qianjia"),
 New clsStation(2213, "新莊", "Xinzhuang"),
 New clsStation(2203, "竹中", "Jhujhong"),
 New clsStation(2214, "六家", "Liujia"),
 New clsStation(2204, "上員", "Shangyuan", 4),
 New clsStation(2205, "竹東", "Jhudong", 2),
 New clsStation(2206, "橫山", "Hengshan", 4),
 New clsStation(2207, "九讚頭", "jiouzantou"),
 New clsStation(2208, "合興", "Hesing", 4),
 New clsStation(2209, "富貴", "Fuguei", 4),
 New clsStation(2210, "內灣", "Neiwan"),
 New clsStation(2211, "榮華", "Ronghua", 4),
 New clsStation(2302, "臺中港貨", "Taichung Port"),
 New clsStation(2402, "龍井煤場", "Longjing"),
 New clsStation(2502, "神岡", "Shangang"),
 New clsStation(2702, "源泉", "Yuanciyuan", 4),
 New clsStation(2703, "濁水", "Jhuoshuei"),
 New clsStation(2704, "龍泉", "Lungcyuan", 4),
 New clsStation(2705, "集集", "Jiji", 5),
 New clsStation(2706, "水里", "Shueili"),
 New clsStation(2707, "車埕", "Checheng"),
 New clsStation(2802, "南調", "Nandiao"),
 New clsStation(2902, "高雄港", "Kaohsiung Port"),
 New clsStation(3102, "前鎮", "na", -1),
 New clsStation(3202, "花蓮港", "hualien Port"),
 New clsStation(3302, "中興一號", "na", -1),
 New clsStation(3402, "中興二號", "na", -1),
 New clsStation(3902, "機廠", "na", -1),
 New clsStation(4102, "樹調", "ShuDiao"),
 New clsStation(4202, "東港支線", "na", -1),
 New clsStation(4302, "東南支線", "na", -1),
 New clsStation(1244, "南科", "Nanke"),
 New clsStation(5101, "長榮大學", "Chang Jung Christian University"),
 New clsStation(5102, "沙崙", "Shalun"),
 New clsStation(1033, "北湖", "BaiHwu"),
 New clsStation(6103, "海科館", "na"),
 New clsStation(1243, "仁德", "Rende")
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
    New clsStGrp(16, "內灣/六家", "taipei", StGrpColor, New List(Of String) From {"新竹", "北新竹", "千甲", "新莊", "竹中", "上員", "榮華", "竹東", "橫山", "九讚頭", "合興", "富貴", "內灣"}, "千甲"),
    New clsStGrp(17, "集集區", "taipei", StGrpColor, New List(Of String) From {"二水", "源泉", "濁水", "龍泉", "集集", "水里", "車埕"}, "集集"),
    New clsStGrp(18, "沙崙區", "taipei", StGrpColor, New List(Of String) From {"長榮大學", "沙崙"}, "沙崙"),
    New clsStGrp(19, "深澳線", "taipei", StGrpColor, New List(Of String) From {"瑞芳", "海科館"}, "瑞芳")
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
