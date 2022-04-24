function getAccessToken(Email, Password) {
    let user = {
        email = Email,
        password = Password
    }
    fetch("https://localhost:7245/api/Authentication/SignIn", {
        method: "post",
        header: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(user)
    })
    .then(res => res.text())
    .then(data => {
        localStorage.setItem("accessToken")
    })
};

export function logInAlert() {
    alert("Succesfully logged in");
}

export function logInErrorAlert() {
    alert("Incorrect Username or Email");
}