var buffer;
function runCode()
{
    LoadCookie()
    DisplayCookie()
    LoadCookieAsObject()
    AlterCookie()
    LoadValues()
}

function LoadCookie()
{
    buffer = document.cookie
}

function DisplayCookie()
{
    root = document.getElementById('Display')
    let node = document.createElement('p');
    node.innerHTML = 'Cookie upon load: '+buffer;
    root.appendChild(node)
}

function LoadCookieAsObject()
{
    buffer = JSON.parse(document.cookie)
}

function AlterCookie()
{
    buffer.GroupId = 1
}

function LoadValues()
{
    root = document.getElementById('Display')
    let node = document.createElement('p');
    node.innerHTML = 'buffer.Value: '+buffer.Value;
    root.appendChild(node)
    root = document.getElementById('Display')
    node = document.createElement('p');
    node.innerHTML = 'buffer.GroupId: '+buffer.GroupId;
    root.appendChild(node)
}