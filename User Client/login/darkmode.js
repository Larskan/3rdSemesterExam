function myFunction() {
  var element = document.body;
  element.classList.toggle("dark-mode");
}

function ValidateEmail() {
  var mail = document.getElementById("email").value;
  var regx = /\S+@\S+\.\S+/;
  if (regx.test(mail)) {
    alert("A valid email");
  } else alert("Not a valid email");
}
