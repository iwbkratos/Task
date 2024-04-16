document.getElementById('form').addEventListener('submit',(event)=>{

    let username=document.getElementById("lusername").value;
    let userpassword=document.getElementById('lpassword').value;
    let exusername;
    let exuserpassword;
    const storeduserJSON=localStorage.getItem('user');
   if(storeduserJSON)
   {
      const storedUser=JSON.parse(storeduserJSON);
      exusername=storedUser.username;
      exuserpassword=storedUser.password;
   }
   
   console.log(storeduserJSON);

    if(username==exusername && exuserpassword==userpassword)
    {
        document.getElementById("form").setAttribute('action',"list.html")
        setCookie("username",username);
        setCookie("userpassword",userpassword);
    }
    else
    {
        alert("login failed");
    }

});


function setCookie(key,value)
{
  let date=new Date();
  date.setTime(date.getTime()+(24*60*60*1000));
  let expires = "expires="+ date.toUTCString();
  document.cookie = key + "=" + value + ";" + expires + ";path=/";
}

 function emailValidator(email)
 {
    let emailcheckExp=/^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/
    if(!emailcheckExp.test(email))
    {
      alert("invalid email");
    }
 }