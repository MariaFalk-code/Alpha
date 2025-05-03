const validateInput = (input) => {
    let errorSpan = document.querySelector(`span[data-valmsg-for="${input.name}"]`);
    if (!errorSpan) return;

    let errorMessage = "";
    let value = input.value.trim();

    if (input.hasAttribute("data-val-required") && value=== "") {
        errorMessage = input.getAttribute("data-val-required");
    }
    if (input.hasAttribute("data-val-regex") && value !== "") {
        let pattern = new RegExp(input.getAttribute("data-val-regex-pattern"));
        if (!pattern.test(value)) {
            errorMessage = input.getAttribute("data-val-regex");
        }
    }
    if (errorMessage) {
        input.classList.add("input-validation-error");
        errorSpan.classList.remove("field-validation-valid")
        errorSpan.classList.add("field-validation-error");
        errorSpan.textContent = errorMessage;
    }
    else {
        input.classList.remove("input-validation-error");
        errorSpan.classList.remove("field-validation-error");
        errorSpan.classList.add("field-validation-valid");
        errorSpan.textContent = "";
    }
    
};

document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector("form");

    if (!form) return;

    const inputs = form.querySelectorAll("input[data-val='true']");

    inputs.forEach(input => {
        input.addEventListener("input", () => {
            validateInput(input);
        })
    })
});