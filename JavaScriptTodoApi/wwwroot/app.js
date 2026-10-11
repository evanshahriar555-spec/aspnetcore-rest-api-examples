const endpoint = '/api/todoitems';
const list = document.querySelector('#items');
const message = document.querySelector('#message');
const input = document.querySelector('#new-name');

async function request(url, options) {
  const response = await fetch(url, options);
  if (!response.ok) throw new Error(`Request failed (${response.status})`);
  return response.status === 204 ? null : response.json();
}
async function load() {
  try {
    message.textContent = '';
    const items = await request(endpoint);
    list.replaceChildren();
    for (const item of items) {
      const row = document.createElement('li');
      if (item.isComplete) row.classList.add('done');
      const checkbox = document.createElement('input');
      checkbox.type = 'checkbox'; checkbox.checked = item.isComplete;
      checkbox.setAttribute('aria-label', `Complete ${item.name}`);
      checkbox.addEventListener('change', async () => {
        try {
          await request(`${endpoint}/${item.id}`, { method: 'PUT', headers: {'Content-Type':'application/json'}, body: JSON.stringify({name:item.name,isComplete:checkbox.checked}) });
          await load();
        } catch (error) { message.textContent = error.message; checkbox.checked = !checkbox.checked; }
      });
      const label = document.createElement('span'); label.textContent = item.name;
      const remove = document.createElement('button'); remove.textContent = 'Delete'; remove.className = 'delete';
      remove.addEventListener('click', async () => {
        try { await request(`${endpoint}/${item.id}`, {method:'DELETE'}); await load(); }
        catch (error) { message.textContent = error.message; }
      });
      row.append(checkbox, label, remove); list.append(row);
    }
  } catch (error) { message.textContent = error.message; }
}
document.querySelector('#add-form').addEventListener('submit', async event => {
  event.preventDefault();
  const name = input.value.trim();
  if (!name) return;
  try {
    await request(endpoint, {method:'POST', headers:{'Content-Type':'application/json'}, body:JSON.stringify({name,isComplete:false})});
    input.value = ''; await load();
  } catch (error) { message.textContent = error.message; }
});
load();
