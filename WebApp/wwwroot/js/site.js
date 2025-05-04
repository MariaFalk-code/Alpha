document.addEventListener("DOMContentLoaded", () => {

    // Rich Text Editor Initialization (Quill). Chat GPT4o provided the code, updated to support multiple editors.
    document.querySelectorAll('.rich-text-editor').forEach(editorElement => {
        if (!editorElement) return;

        const quill = new Quill(editorElement, {
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

        const form = editorElement.closest('form');
        const hiddenInput = form?.querySelector('input[name="Description"]');

        // Load initial content if any
        const initial = editorElement.getAttribute("data-initial-content");
        if (initial) {
            quill.root.innerHTML = initial;
        }

        // On form submit, push content to hidden field
        if (form && hiddenInput) {
            form.addEventListener('submit', () => {
                hiddenInput.value = quill.root.innerHTML;
            });
        }

        // Store reference
        editorElement.__quill = quill;
    });

    //Script for handling closing currently open dropdowns when a new one is opened. Used in addition to material tailwind.
    //Code provided by ChatGPT4o and altered after my suggestions.
    document.querySelectorAll('[data-popover-target]').forEach(trigger => {
        trigger.addEventListener('click', (e) => {
            const targetId = trigger.getAttribute('data-popover-target');
            const targetPopover = document.querySelector(`[data-popover="${targetId}"]`);

            // Close all other popovers before opening the new one
            document.querySelectorAll('[data-popover]').forEach(popover => {
                if (popover !== targetPopover) {
                    const unmount = popover.getAttribute('data-popover-unmount')?.split(' ') ?? [];
                    const mount = popover.getAttribute('data-popover-mount')?.split(' ') ?? [];

                    popover.classList.remove(...mount);
                    popover.classList.add(...unmount);
                }
            });
        });
    });

});

