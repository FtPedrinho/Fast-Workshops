function renderCollaboratorCard(colaborador) {
    return `
        <article class="collaborator-card">

            <div class="collaborator-info">
                <span class="collaborator-id">
                    #${colaborador.id}
                </span>

                <h2>${colaborador.nome}</h2>
            </div>

            <div class="card-actions">

                <button
                    type="button"
                    class="icon-button edit-button"
                    data-action="edit"
                    data-id="${colaborador.id}"
                    aria-label="Editar ${colaborador.nome}"
                    title="Editar colaborador"
                >
                    ✎
                </button>

                <button
                    type="button"
                    class="icon-button delete-button"
                    data-action="delete"
                    data-id="${colaborador.id}"
                    aria-label="Excluir ${colaborador.nome}"
                    title="Excluir colaborador"
                >
                    🗑
                </button>

            </div>

        </article>
    `;
}

export {
    renderCollaboratorCard
};