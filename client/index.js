document.addEventListener("DOMContentLoaded", init);

const API_URL = "http://localhost:5032/api/users";

const elements = {
  tableBody: document.querySelector(".js-user-table-body"),
  userCountSpan: document.querySelector(".js-user-count"),

  form: document.querySelector(".js-form"),
  addUserBtn: document.querySelector(".js-add-user-btn"),

  addLocallyBtn: document.querySelector(".js-add-locally-btn"),
  addOnServerBtn: document.querySelector(".js-add-on-server-btn"),

  nameInput: document.querySelector(".js-form-input-name"),
  ageInput: document.querySelector(".js-form-input-age"),
};

let userCount = 0;
let lastUserId = 0;

async function init() {
  const data = await UserService.fetchUsers();
  userCount = data.st;
  renderUsers(data.seznam);

  elements.addUserBtn.addEventListener('click', () => elements.form.style.display = "block");
  window.addEventListener('click', (event) => {
    if (event.target == elements.form) {
      elements.form.style.display = "none";
    }
  });

  elements.addLocallyBtn.addEventListener('click', handleLocalUserAdd);
  elements.addOnServerBtn.addEventListener('click', handleServerUserAdd);
}

// API
const UserService = {
  async fetchUsers() {
    try {
      const response = await fetch(API_URL);
      
      if (!response.ok) throw new Error("Failed to fetch users");
      
      const data = await response.json();

      return data;

    } catch (error) {
      console.log("Unexpected error:", error);
    }
  },

  async addUser(name, age) {
    try {
      const response = await fetch(API_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ ime: name, starost: parseInt(age) })
      });

      if (!response.ok) throw new Error("Failed to add user");

    } catch (error) {
      console.log("Unexpected error:", error);
    }
  }
};

function renderUsers(users) {
  elements.tableBody.innerHTML = "";
  elements.userCountSpan.innerHTML = userCount;
  users.forEach(addUserToTable);
}

function addUserToTable(user) {
  lastUserId = user.id;

  const newRow = document.createElement("tr");
  newRow.innerHTML += `
    <td>${user.id}</td>
    <td>${user.ime}</td>
    <td>${user.starost}</td>
  `;
  elements.tableBody.appendChild(newRow);
}

function handleLocalUserAdd() {
  const name = elements.nameInput.value.trim();
  const age = elements.ageInput.value.trim();

  if (!validateInput(name, age)) return;
  
  addUserToTable({ id: ++lastUserId, ime: name, starost: age });
  
  closeForm();
}

async function handleServerUserAdd() {
  const name = elements.nameInput.value.trim();
  const age = elements.ageInput.value.trim();

  if (!validateInput(name, age)) return;

  await UserService.addUser(name, age);

  closeForm();
}

function validateInput(name, age) {
  if (!name || ![...name].some(char => isNaN(char) && char !== " ")) {
    alert("Napačno ime.");
    return false;
  }

  if (!age || isNaN(age) || age < 1 || age > 120) {
    alert("Napačna starost.");
    return false;
  }

  return true;
}

function closeForm() {
  elements.form.style.display = "none";
  elements.nameInput.value = "";
  elements.ageInput.value = "";
}