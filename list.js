
let limit=20;
let list=document.querySelectorAll(".list");
let currentpage=1;

function run()
{
  
  document.getElementById("pgno").style.display="";
  limit=document.getElementById("count-limit").value;
  load();
}
 
 console.log(limit);
 
function load()
{
  let starting=limit*(currentpage-1)
  let ending=limit*currentpage-1;
  for(let i=0;i<list.length;i++)
  {
        if( i>=starting&&i<=ending)
        {
            list[i].style.display="";
        }
        else
        {
          list[i].style.display="none";
        }
  }
 listpage();
}
load();

function listpage()
{
  const count=Math.ceil(list.length/limit);
  document.getElementById("pgno").innerHTML="";
  if(currentpage!=1)
  {
    let newSpan=document.createElement('span');
      newSpan.innerHTML="&laquo";
      newSpan.setAttribute('onclick',"changePage("+(currentpage-1)+")");
      document.getElementById('pgno').appendChild(newSpan);
  }
  for(let i=1;i<=count;i++)
  {
    let newSpan=document.createElement('span');
       newSpan.innerHTML=i;
       newSpan.setAttribute('onclick','changePage('+i+')');
       document.getElementById('pgno').appendChild(newSpan);
  }
  if(currentpage!=count)
  {
    let newSpan=document.createElement('span');
      newSpan.innerHTML="&raquo";
      newSpan.setAttribute('onclick',"changePage("+(currentpage+1)+")");
      document.getElementById('pgno').appendChild(newSpan);
  }
}
function changePage(i)
{
  currentpage=i;
  load();
}

function getUsername()
{
     let exusername= localStorage.getItem("username1");
     return String(exusername);
}

function deleteCookie()
{
  let cookiename=getUsername();
  // let cookies=document.cookie.split(";");
  // console.log(cookiename);
  let date=new Date();
  date.setTime(date.setTime(2020, 3, 10));
    
        document.cookie= 'username=;'+date.toUTCString()+';path=/';
        document.cookie = 'userpassword=;'+date.toUTCString()+';path=/';
}
