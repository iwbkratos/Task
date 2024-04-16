document.getElementById('form').addEventListener('submit',(e)=>{
    e
    .preventDefault()

    
    const username=document.getElementById("email").value;
    emailValidator(username);
    const userpassword=document.getElementById('password').value;
   if(username!=null && userpassword!=null)
   {
    const user={
        username:username,
        password:userpassword
    }

    const userJSON=JSON.stringify(user);
    localStorage.setItem('user',userJSON);
}
  
});
   
function emailValidator(email)
 {
    let emailcheckExp=/^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/
    if(!emailcheckExp.test(email))
    {
      alert("invalid email");
    }
    else{
        window.location.href='login.html';
    }

 }