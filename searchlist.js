const container=document.getElementsByClassName("list");
function sort()
{
    document.getElementById('pgno').style.display="none";
const input=document.getElementById("input").value;
let search=new RegExp(input,"i");
for(let i=0;i<container.length;i++)
{
    const job=container[i].querySelectorAll("p");
    let flag=0;
for (const txt of job) {
    console.log( txt.innerHTML);
   if(txt.innerHTML.match(search)!=null)
   {
       console.log("true")
       container[i].style.display="";
       flag=1;
       continue;
   }

}
if(flag==0)
{
    container[i].style.display="none";
}
flag =0;
}
}


