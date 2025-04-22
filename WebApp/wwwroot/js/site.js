//Script for handling switching between dialogs, provided by ChatGPT4o and slightly modified by me to actually work. 

document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll('[data-switch-dialog]').forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();

            const from = link.getAttribute('data-close');
            const to = link.getAttribute('data-switch-dialog');

            const backdropFrom = document.querySelector(`[data-dialog-backdrop="${from}"]`);
            const dialogFrom = document.querySelector(`[data-dialog="${from}"]`);
            const backdropTo = document.querySelector(`[data-dialog-backdrop="${to}"]`);
            const dialogTo = document.querySelector(`[data-dialog="${to}"]`);

            if (!backdropFrom || !dialogFrom || !backdropTo || !dialogTo) return;

            // Close current modal
            backdropFrom.classList.add("opacity-0", "pointer-events-none");
            dialogFrom.classList.remove("opacity-1", "translate-y-0");
            dialogFrom.classList.add("opacity-0", "-translate-y-14");

            // Small delay to allow closing animation to play
            setTimeout(() => {
                // Open new modal
                backdropTo.classList.remove("opacity-0", "pointer-events-none");
                dialogTo.classList.add("opacity-1", "translate-y-0");
                dialogTo.classList.remove("opacity-0", "-translate-y-14");
            }, 250); // Matches Tailwind's transition duration
        });
    });
});

