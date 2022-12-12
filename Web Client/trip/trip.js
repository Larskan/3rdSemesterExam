function LoadCookie() {
    const node = document.getElementById("expenses");

    let temp = document.createElement("p");
    temp.innerHTML = document.cookie;
    node.appendChild(temp);
}
  
function logCookie()
{
  //document.cookie = '123';
  console.log(document.cookie)
}