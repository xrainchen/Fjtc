function ShowHideChannel(obj) {
    var sdkType = $(obj).val();
    switch (sdkType) {
        case "KaoPuSDK":
            $('#divMainChannel',navTab.getCurrentPanel()).removeClass('hide');
            $('#divSubChannel', navTab.getCurrentPanel()).removeClass('hide');
            $('#SubChannelId', navTab.getCurrentPanel()).val('');
            $('#SubChannelName', navTab.getCurrentPanel()).val('');
            $('#UserType', navTab.getCurrentPanel()).parent().removeClass('hide');
            break;

        case "YiWanSDK":
            $('#divMainChannel', navTab.getCurrentPanel()).addClass('hide');
            $('#divSubChannel', navTab.getCurrentPanel()).addClass('hide');
            $('#SubChannelId', navTab.getCurrentPanel()).val('');
            $('#SubChannelName', navTab.getCurrentPanel()).val('');
            $('#UserType', navTab.getCurrentPanel()).parent().addClass('hide');
            break;

    }
}
//********************************************************************
var Tip_LoginCount = "登录人数:某段时间内有登录的用户，根据用户登录时间来计算";
var Tip_NewAddUserCount = "新增用户： 新增登录用户数，根据用户首次登录时间来计算";
var Tip_PayCount = "付费人数： 用户充值+赠送充值+代金券充值的人数 （去重）";
var Tip_NewUserPayCount = "新用户付费人数： 新增用户中付费人数";
var Tip_RechargeAmount = "充值金额： 用户充值金额+赠送充值金额+代金券充值金额，三部分数据之和";
var Tip_PayRate = "付费率： 活跃用户的付费比例,计算方法:付费人数/登录人数*100%";
var Tip_ARPU = "ARPU： 计算方法：充值金额/登录人数";
var Tip_ARPPU = "ARPPU： 计算方法：充值金额/付费人数";
var Tip_GrowDays = "成长天数：（最后登录时间与首次登录时间的天数，与搜索时填的统计时间无关）";
var Tip_NewUserRechargeAmount = "新用户充值金额:新增用户中充值金额";
var Tip_RealUserRechargeAmount = "实际用户充值人数： 用户直充（不包含返利）金额+平台币（用户充值部分）充值到游戏的金额";
var Tip_UserRechargeAmount = "用户充值金额： 用户直充（不包含返利）金额+平台币（用户充值部分）充值到游戏的金额";
var Tip_GiveRechargeAmount = "赠送充值金额： 平台币（赠送）充值到游戏的金额+后台返利充值的金额";
var Tip_VoucherRechargeAmount = "代金券充值金额： 代金券充值到游戏的金额";
var Tip_MorrowDayKeepCount = "次日留存人数： (第一天新注册的用户中，在第二天还有登录记录的用户数)";
var Tip_MorrowDayKeepRate = "次日留存率： (第一天新注册的用户中，在第二天还有登录记录的用户数)/ 第一天新注册的用户数";
var Tip_LTV1 = "LTV1：（第1天时间内）这部分用户累计充值金额/ 第一天新增登录总用户数";
var Tip_LTV2 = "LTV2：（第1天至第2天时间段内）这部分用户累计充值金额/ 第一天新增登录总用户数";
var Tip_LTV3 = "LTV3：（第1天至第3天时间段内）这部分用户累计充值金额/ 第一天新增登录总用户数";
var Tip_LTV4 = "LTV4：（第1天至第4天时间段内）这部分用户累计充值金额/ 第一天新增登录总用户数";
var Tip_LTV5 = "LTV5：（第1天至第5天时间段内）这部分用户累计充值金额/ 第一天新增登录总用户数";
var Tip_LTV6 = "LTV6：（第1天至第6天时间段内）这部分用户累计充值金额/ 第一天新增登录总用户数";
var Tip_LTV7 = "LTV7：（第1天至第7天时间段内）这部分用户累计充值金额/ 第一天新增登录总用户数";
//********************************************************************

//==========表格排序===========
function ResetSort(defValue) {
    $(".slimtable-sprites").each(function () {
        $(this).removeClass('slimtable-sortasc').removeClass('slimtable-sortdesc').addClass("slimtable-sortboth");
        if (defValue == $(this).attr("data-sort")) {
            $(this).removeClass('slimtable-sortboth').addClass("slimtable-sortdesc");
            $("input[name='orderField']", navTab.getCurrentPanel()).val(defValue)
            $("input[name='orderDirection']", navTab.getCurrentPanel()).val("DESC")
        }
    });
}
function TableSort(obj) {
    $(".slimtable-sprites").each(function () {
        $(this).removeClass('slimtable-sortasc').removeClass('slimtable-sortdesc').addClass("slimtable-sortboth");
    });
    //同一列
    if ($(obj).attr("data-sort") == $("input[name='orderField']", navTab.getCurrentPanel()).val()) {
        if ($("input[name='orderDirection']", navTab.getCurrentPanel()).val() == "DESC") {
            $("input[name='orderDirection']", navTab.getCurrentPanel()).val("ASC")
            $(obj).removeClass('slimtable-sortboth').addClass("slimtable-sortasc");
        } else {
            $("input[name='orderDirection']", navTab.getCurrentPanel()).val("DESC")
            $(obj).removeClass('slimtable-sortboth').addClass("slimtable-sortdesc");
        }
    } else {
        $("input[name='orderField']", navTab.getCurrentPanel()).val($(obj).attr("data-sort"))
        $("input[name='orderDirection']", navTab.getCurrentPanel()).val("DESC")
        $(obj).removeClass('slimtable-sortboth').addClass("slimtable-sortdesc");
    }
    $("#btnSearch", navTab.getCurrentPanel()).click();
}
//********************************************************************
function ChangeLinkUrl(obj, url, selectedId, selectedName) {
    var SdkType = $('#tbAdvancedQuery input[name="SdkType"]:checked ', navTab.getCurrentPanel()).val();
    var ids = $('#' + selectedId, navTab.getCurrentPanel()).val();
    var names = $('#' + selectedName, navTab.getCurrentPanel()).val();
    //$(obj).attr("href", url + "&SdkType=" + SdkType + "&SelectedIds=" + ids + "&SelectedNames=" + names);
    $(obj).attr("href", url + "&SdkType=" + SdkType + "&SelectedIds=" + ids);
}

//navTab.getCurrentPanel()
//$.pdialog.getCurrent()
//********************************************************************
function pdialogChangeLinkUrl(obj, url, selectedId, selectedName) {
    try {
        var SdkType = $('#tbAdvancedQuery input[name="SdkType"]:checked ', $.pdialog.getCurrent()).val();
        //alert("SdkType:" + SdkType);
        var ids = $('#' + selectedId, $.pdialog.getCurrent()).val();
        //alert("ids:" + ids);
        var names = $('#' + selectedName, $.pdialog.getCurrent()).val();
        //alert("names:" + names);
        //$(obj).attr("href", url + "&SdkType=" + SdkType + "&SelectedIds=" + ids + "&SelectedNames=" + names);
        $(obj).attr("href", url + "&SdkType=" + SdkType + "&SelectedIds=" + ids);
    } catch (e) {
        alert(e.message);
    }
    
}

