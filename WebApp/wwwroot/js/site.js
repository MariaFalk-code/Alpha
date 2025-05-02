document.addEventListener("DOMContentLoaded", () => {
    //Script for handling switching between dialogs, provided by ChatGPT4o and slightly modified by me to actually work.
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

            setTimeout(() => {
                backdropTo.classList.remove("opacity-0", "pointer-events-none");
                dialogTo.classList.add("opacity-1", "translate-y-0");
                dialogTo.classList.remove("opacity-0", "-translate-y-14");
            }, 250);
        });
    });

    // //Script for handling the rich context editor, provided by ChatGPT4o. Updated to also load initial content into editor if present when editing.
    const editorElement = document.querySelector('#editor');
    const form = document.querySelector('form');

    if (editorElement && form) {
        const quill = new Quill('#editor', {
            theme: 'snow',
            placeholder: 'Write your project description',
            modules: {
                toolbar: [
                    [{ header: [1, 2, false] }],
                    ['bold', 'italic', 'underline'],
                    ['link', 'blockquote', 'code-block'],
                    [{ list: 'ordered' }, { list: 'bullet' }],
                    ['clean']
                ]
            }
        });

        const initialContent = editorElement.dataset.initialContent;
        if (initialContent) {
            quill.root.innerHTML = initialContent;
        }

        form.addEventListener('submit', function () {
            const content = document.querySelector('#description');
            content.value = quill.root.innerHTML;
        });
    }

});

