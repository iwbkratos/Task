var allCookies = document.cookie;
console.log(allCookies);
var cookiesArray = allCookies.split("; ");
for (var i = 0; i < cookiesArray.length; i++) {
  var cookie = cookiesArray[i];
  var cookieParts = cookie.split("=");
  var cookiename = cookieParts[0];
  var cookieValue = cookieParts[1];
  if (
    cookiename == null ||
    cookiename == "" ||
    cookieValue == null ||
    cookieValue == ""
  ) {
    window.location.href = "login.html";
  }
}
// -----------------------------------------------------------------------------------------
let limit = 20;
let list;
let currentpage = 1;

function filter() {
  if (typeof list === "undefined") {
    list = document.querySelectorAll(".list");
  }
  run();
}
function search() {
  list = null;
  sort();
  list = sort();
  run();
}

function sort() {
  const container = document.getElementsByClassName("list");
  let cardArray = [];
  document.getElementById("pgno").style.display = "none";
  const input = document.getElementById("input").value;
  if(input==""|| input==null || input=='undefined'||input.trim(" ")=="")
  {
    input="";
     return null;
  }
  let search = new RegExp(input, "i");
  for (let i = 0; i < container.length; i++) {
    const job = container[i].querySelectorAll("p");
    let flag = 0;
    for (const txt of job) {
      console.log(txt.innerHTML);
      if (txt.innerHTML.match(search) != null) {
        console.log("true");
        //  container[i].style.display="";
        flag = 1;
        cardArray.push(container[i]);
        continue;
      }
    }
    if (flag == 0) {
      container[i].style.display = "none";
    }
    flag = 0;
  }
  return cardArray;
}

// ----------------------------------------------------------------------------------------------
function run() {
  document.getElementById("pgno").style.display = "";
  limit = document.getElementById("count-limit").value;
  currentpage = 1;
  load();
}

console.log(limit);

function load() {
  let starting = limit * (currentpage - 1);
  let ending = limit * currentpage - 1;
  if(list!=null)
  {
    for (let i = 0; i < list.length; i++) {
      if (i >= starting && i <= ending) {
        list[i].style.display = "";
      } else {
        list[i].style.display = "none";
      }
    }
    listpage();
  } 

}

// load();

function listpage() {
  const count = Math.ceil(list.length / limit);
  document.getElementById("pgno").innerHTML = "";
  if (currentpage != 1) {
    let newSpan = document.createElement("span");
    newSpan.innerHTML = "&laquo";
    newSpan.setAttribute("onclick", "changePage(" + (currentpage - 1) + ")");
    document.getElementById("pgno").appendChild(newSpan);
  }
  for (let i = 1; i <= count; i++) {
    let newSpan = document.createElement("span");
    newSpan.innerHTML = i;
    newSpan.setAttribute("onclick", "changePage(" + i + ")");
    if (i == currentpage) {
      newSpan.style.backgroundColor = "blue";
      newSpan.style.color = "white";
    }
    document.getElementById("pgno").appendChild(newSpan);
  }
  if (currentpage != count) {
    let newSpan = document.createElement("span");
    newSpan.innerHTML = "&raquo";
    newSpan.setAttribute("onclick", "changePage(" + (currentpage + 1) + ")");
    document.getElementById("pgno").appendChild(newSpan);
  }
}
function changePage(i) {
  currentpage = i;
  load();
}


function deleteCookie() {
  let cookiename = "userCookie";
  let date = new Date();
  date.setTime(date.setTime(2020, 3, 10));
  document.cookie = "username=;" + date.toUTCString() + ";path=/";
  document.cookie = "userpassword=;" + date.toUTCString() + ";path=/";
}
