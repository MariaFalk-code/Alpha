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

    //Script for handling the rich context editor, provided by ChatGPT4o. Updated to support multiple editors.
    document.querySelectorAll('.rich-text-editor').forEach(editorElement => {
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

        editorElement.__quill = quill;
        const form = editorElement.closest('form');
        const hiddenInput = form?.querySelector(`input[name="Description"]`);


        if (form && hiddenInput) {
            form.addEventListener('submit', () => {
                hiddenInput.value = quill.root.innerHTML;
            });
        }
    });

    //Script for handling closing currently open dropdowns when a new one is opened. Code provided by ChatGPT4o and altered after my suggestions.
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

    //Script for opening the edit project modal manually from dropdown, since the standard logic won't work because of the nesting. 
    //Added functionality to pre-fill form fields with existing values. Code provided by ChatGPT4o, modified several times to function properly.
    document.addEventListener("click", (e) => {
        const modalTrigger = e.target.closest('.open-edit-project-modal');
        if (!modalTrigger) return;

        e.stopPropagation();

        const dialog = document.querySelector('[data-dialog="edit-project-dialog"]');
        const backdrop = document.querySelector('[data-dialog-backdrop="edit-project-dialog"]');
        if (!dialog || !backdrop) return;

        // Fill form fields from data attributes
        dialog.querySelector('[data-target="edit-project-id"]').value = modalTrigger.dataset.projectId ?? '';
        dialog.querySelector('[data-target="edit-project-name"]').value = modalTrigger.dataset.projectName ?? '';
        dialog.querySelector('[data-target="edit-client-name"]').value = modalTrigger.dataset.clientName ?? '';
        dialog.querySelector('[data-target="edit-start-date"]').value = modalTrigger.dataset.startDate ?? '';
        dialog.querySelector('[data-target="edit-end-date"]').value = modalTrigger.dataset.endDate ?? '';
        dialog.querySelector('[data-target="edit-budget"]').value = modalTrigger.dataset.budget ?? '';

        const richTextEditor = dialog.querySelector('.rich-text-editor');
        if (richTextEditor && modalTrigger.dataset.description) {
            const quill = richTextEditor.__quill;
            if (quill) {
                quill.setContents(quill.clipboard.convert(modalTrigger.dataset.description));
            }
        }

        // Show modal
        setTimeout(() => {
            backdrop.classList.remove("opacity-0", "pointer-events-none");
            dialog.classList.remove("opacity-0", "-translate-y-14");
            dialog.classList.add("opacity-1", "translate-y-0");
        }, 200);
    });

    //Script for closing the edit project modal when clicking outside of it. Code provided by ChatGPT4o.
    document.addEventListener('click', (e) => {
        const dialog = document.querySelector('[data-dialog="edit-project-dialog"]');
        const backdrop = document.querySelector('[data-dialog-backdrop="edit-project-dialog"]');

        if (!dialog || !backdrop) return;

        const isDialogOpen = dialog.classList.contains('opacity-1');

        if (
            isDialogOpen &&
            !dialog.contains(e.target) &&
            !e.target.closest('[data-popover]')
        ) {
            // Close modal
            backdrop.classList.add("pointer-events-none", "opacity-0");
            dialog.classList.remove("opacity-1", "translate-y-0");
            dialog.classList.add("opacity-0", "-translate-y-14");
        }
    });

});

