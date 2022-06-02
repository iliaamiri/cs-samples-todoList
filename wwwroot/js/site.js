// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const body = document.querySelector("body");

body.addEventListener('click', async (event) => {
    let target = event.target;

    if (target.tagName === "A" && target.classList.contains("todoList-done")) {
        let todoItemId = Number(target.dataset.itemId);
        console.log(todoItemId)
        try {
            await axios.post("/TodoItem/MarkAsDone/" + todoItemId);

            let closestTd = target.closest("tr");
            let isCompleted = closestTd.querySelector("td.isCompleteText");
            isCompleted.innerHTML = "Yes";
        } catch (error) {
            console.log(error);
            console.log("error status code: ", error.response.status);
        }
    }
});
