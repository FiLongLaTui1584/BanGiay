const logregBox = document.querySelector('.logreg-box');
const loginlink = document.querySelector('.login-link');
const registerlink = document.querySelector('.register-link');
const forgotpasslink = document.querySelector('.forgotpass-link');
const loginlinkagain = document.querySelector('.login-linkagain');


registerlink.addEventListener('click', () => {
    logregBox.classList.add('active');
});

loginlink.addEventListener('click', () => {
    logregBox.classList.remove('active');
});

forgotpasslink.addEventListener('click', () => {
    logregBox.classList.add('active');
});

loginlinkagain.addEventListener('click', () => {
    logregBox.classList.remove('active');
});