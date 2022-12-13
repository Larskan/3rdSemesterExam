const username = document.getElementById("email");
const loginBtn = document.querySelector("#LoginBtn");

loginBtn.addEventListener("click", () => {
  setCookie("username", username.value, 365);
});

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

function setCookie(name, value, daysToLive) {
  const date = new Date();
  date.setTime(date.getTime() + daysToLive * 24 * 60 * 60 * 1000);
  let expires = "expires=" + date.toUTCString();
  document.cookie = `${name}=${value}; ${expires}; path=/`;
}

function deleteCookie(name) {
  setCookie(name, null, null);
}

function getCookie(name) {
  const cDecoded = decodeURIComponent(document.cookie);
  const cArray = cDecoded.split("; ");
  let result = null;

  cArray.forEach((element) => {
    if (element.indexOf(name) == 0) {
      result = element.substring(name.length + 1);
    }
  });
  return result;
}
