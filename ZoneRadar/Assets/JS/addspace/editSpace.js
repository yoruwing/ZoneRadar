﻿//停車場的 是否
var yesflag = 1;
var parkingPart = document.querySelector("#parkingPart");
let flexSwitchCheckDefaultYes = document.querySelector("#flexSwitchCheckDefaultYes");
flexSwitchCheckDefaultYes.addEventListener("click", function () {

    if (yesflag == 0) {
        parkingPart.classList.remove('d-none');
        yesflag = 1;
    }
    else {
        parkingPart.classList.add('d-none');
        yesflag = 0;
    }
});
//攝影機 是
let projectionflag =1;
let Yesprojection = document.querySelector("#Yesprojection");
var projectionPart = document.querySelector('#projectionPart');
Yesprojection.addEventListener('click', function () {
    if (projectionflag == 0) {

        projectionPart.classList.remove('d-none');
        projectionflag = 1;
    } else {
        projectionPart.classList.add('d-none');
        projectionflag = 0;
    }

});
/營業時間 radio///
var flag = 1;
//monday
//營業
let flexSwitchCheckDefault = document.querySelectorAll("#flexSwitchCheckDefault")[0];
//全天
let flexRadioDefault1 = document.querySelectorAll('#flexRadioDefault1')[0];
//小時
let flexRadioDefault2 = document.querySelectorAll('#flexRadioDefault1')[1];

//開始時間
let StateMon = document.querySelectorAll("#State")[0];
//結束時間
let StateMon2 = document.querySelectorAll("#State")[1];
flexSwitchCheckDefault.addEventListener("click", function () {
    if (flag == 1) {
        flexRadioDefault1.disabled = false;
        flexRadioDefault2.disabled = false;
        flag = 0;
    } else {
        flexRadioDefault1.disabled = true;
        flexRadioDefault2.disabled = true;
        StateMon.disabled = true;
        StateMon2.disabled = true;
        flexRadioDefault2.checked = false;
        flexRadioDefault1.checked = false;
        flag = 1;
    }
});
flexRadioDefault1.addEventListener("click", function () {
    StateMon.disabled = true;
    StateMon2.disabled = true;
});

flexRadioDefault2.addEventListener('click', function () {

    let StateMon = document.querySelectorAll("#State")[0];
    let StateMon2 = document.querySelectorAll("#State")[1];
    StateMon.disabled = false;
    StateMon2.disabled = false;
});


//tuesday
var flagtue=1;
let flexSwitchCheckDefaultTue = document.querySelectorAll("#flexSwitchCheckDefault")[1];
let TueAllDay = document.querySelectorAll('#flexRadioDefault1')[2];
let Tuehour = document.querySelectorAll('#flexRadioDefault1')[3];
flexSwitchCheckDefaultTue.addEventListener("click", function () {
    if (flagtue == 1) {
        TueAllDay.disabled = false;
        Tuehour.disabled = false;
        flagtue = 0;
    } else {
        TueAllDay.disabled = true;
        Tuehour.disabled = true;
        TueAllDay.checked = false;
        Tuehour.checked = false;
        let StateTue = document.querySelectorAll("#State")[2];
        let StateTue2 = document.querySelectorAll("#State")[3];
        StateTue.disabled = true;
        StateTue2.disabled = true;
        flagtue = 1;
    }
});
TueAllDay.addEventListener('click', function () {

    let StateTue = document.querySelectorAll("#State")[2];
    let StateTue2 = document.querySelectorAll("#State")[3];
    StateTue.disabled = true;
    StateTue2.disabled = true;

});
Tuehour.addEventListener('click', function () {

    let StateTue = document.querySelectorAll("#State")[2];
    let StateTue2 = document.querySelectorAll("#State")[3];
    StateTue.disabled = false;
    StateTue2.disabled = false;
})

//wednesday
let flexSwitchCheckDefaultWed = document.querySelectorAll("#flexSwitchCheckDefault")[2];
let WedAllDay = document.querySelectorAll("#flexRadioDefault1")[4];
let Wedhour = document.querySelectorAll("#flexRadioDefault1")[5];
var flagWed;
flexSwitchCheckDefaultWed.addEventListener("click", function () {
    if ((flagWed == 0)) {
        Wedhour.disabled = false;
        WedAllDay.disabled = false;
        flagWed = 1;
    } else {
        Wedhour.disabled = true;
        WedAllDay.disabled = true;
        WedAllDay.checked = false;
        Wedhour.checked = false;
        let StateWed = document.querySelectorAll("#State")[4];
        let StateWed2 = document.querySelectorAll("#State")[5];

        //選擇時間
        //
        StateWed.disabled = true;
        StateWed2.disabled = true;
        flagWed = 0;
    }
});
WedAllDay.addEventListener('click', function () {
    let StateWed = document.querySelectorAll("#State")[4];
    let StateWed2 = document.querySelectorAll("#State")[5];
    StateWed.disabled = true;
    StateWed2.disabled = true;
});
Wedhour.addEventListener('click', function () {
    let StateWed = document.querySelectorAll("#State")[4];
    let StateWed2 = document.querySelectorAll("#State")[5];
    StateWed.disabled = false;
    StateWed2.disabled = false;
});
//thursday
let flexSwitchCheckDefaultThu = document.querySelectorAll("#flexSwitchCheckDefault")[3];
let ThuAllDay = document.querySelectorAll("#flexRadioDefault1")[6];
let Thuhour = document.querySelectorAll("#flexRadioDefault1")[7];
let StateThu = document.querySelectorAll("#State")[6];
let StateThu2 = document.querySelectorAll("#State")[7];
var flagThu;
flexSwitchCheckDefaultThu.addEventListener("click", function () {
    if (flagThu == 0) {
        ThuAllDay.disabled = false;
        Thuhour.disabled = false;
        flagThu = 1;
    } else {
        ThuAllDay.disabled = true;
        Thuhour.disabled = true;
        ThuAllDay.checked = false;
        Thuhour.checked = false;
        StateThu.disabled = true;
        StateThu2.disabled = true;
        flagThu = 0;
    }
});
ThuAllDay.addEventListener('click', function () {
    StateThu.disabled = true;
    StateThu2.disabled = true;
})
Thuhour.addEventListener("click", function () {
    StateThu.disabled = false;
    StateThu2.disabled = false;
})

//friday
let flexSwitchCheckDefaultFri = document.querySelectorAll("#flexSwitchCheckDefault")[4];
let FridayAllDay = document.querySelectorAll("#flexRadioDefault1")[8];
let Fridayhour = document.querySelectorAll("#flexRadioDefault1")[9];
let StateFri = document.querySelectorAll("#State")[8];
let StateFri2 = document.querySelectorAll('#State')[9];
var flagFri;
flexSwitchCheckDefaultFri.addEventListener('click', function () {
    if (flagFri == 0) {
        FridayAllDay.disabled = false;
        Fridayhour.disabled = false;
        flagFri = 1;
    } else {
        FridayAllDay.disabled = true;
        Fridayhour.disabled = true;
        StateFri.disabled = true;
        StateFri2.disabled = true;
        FridayAllDay.checked = false;
        Fridayhour.checked = false;
        flagFri = 0;
    }
});
FridayAllDay.addEventListener("click", function () {
    StateFri.disabled = true;
    StateFri2.disabled = true;
});
Fridayhour.addEventListener("click", function () {
    StateFri.disabled = false;
    StateFri2.disabled = false;
})

//saturday
let flexSwitchCheckDefaultSat = document.querySelectorAll("#flexSwitchCheckDefault")[5];
let SatAllday = document.querySelectorAll("#flexRadioDefault1")[10];
let Sathour = document.querySelectorAll('#flexRadioDefault1')[11];
let StateSat = document.querySelectorAll("#State")[10];
let StateSat2 = document.querySelectorAll("#State")[11];
var flagSat;
flexSwitchCheckDefaultSat.addEventListener('click', function () {
    if ((flagSat == 0)) {
        SatAllday.disabled = false;
        Sathour.disabled = false;
        flagSat = 1;
    } else {
        SatAllday.disabled = true;
        Sathour.disabled = true;
        SatAllday.checked = false;
        Sathour.checked = false;
        StateSat.disabled = true;
        StateSat2.disabled = true;
        flagSat = 0;
    };
});
SatAllday.addEventListener("click", function () {
    StateSat.disabled = true;
    StateSat2.disabled = true;
});
Sathour.addEventListener('click', function () {
    StateSat.disabled = false;
    StateSat2.disabled = false;
});

//sunday 
let flexSwitchCheckDefaultSun = document.querySelectorAll("#flexSwitchCheckDefault")[6];
let SunAllDay = document.querySelectorAll("#flexRadioDefault1")[12];
let Sunhour = document.querySelectorAll('#flexRadioDefault1')[13];
let StateSun = document.querySelectorAll('#State')[12];
let StateSun2 = document.querySelectorAll("#State")[13];
var flagSun;
flexSwitchCheckDefaultSun.addEventListener("click", function () {
    if (flagSun == 0) {
        SunAllDay.disabled = false;
        Sunhour.disabled = false;
        flagSun = 1;
    } else {
        SunAllDay.disabled = true;
        Sunhour.disabled = true;
        SunAllDay.checked = false;
        Sunhour.checked = false;
        StateSun.disabled = true;
        StateSun2.disabled = true;
        flagSun = 0;
    }
});
SunAllDay.addEventListener("click", function () {
    StateSun.disabled = true;
    StateSun2.disabled = true;
});
Sunhour.addEventListener('click', function () {
    StateSun.disabled = false;
    StateSun2.disabled = false;
});


CKEDITOR.replace('textPartType');
CKEDITOR.replace('textPartRule');
CKEDITOR.replace('textPartPark');
CKEDITOR.replace('textPartProjection');
CKEDITOR.replace('textPartClean');
CKEDITOR.replace('textPartTransportation');


///上傳照片
FilePond.parse(document.body);