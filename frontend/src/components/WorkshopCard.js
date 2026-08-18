function renderWorkshopCard(workshop) {
    const formattedDate = formatDate(workshop.dataRealizacao);

    const participantCount =
        workshop.participantes?.length ?? 0;

    return `
        <article class="workshop-card">

            <div class="workshop-card-header">
                <span class="workshop-date">
                    ${formattedDate}
                </span>
            </div>

            <div class="workshop-card-content">

                <h2>
                    <a
                        href="#/workshops/${workshop.id}"
                        class="workshop-title"
                    >
                        ${workshop.nome}
                    </a>
                </h2>

                <p>
                    ${workshop.descricao || "Sem descrição."}
                </p>

                <span class="workshop-participants">
                    ${participantCount}
                    participante${participantCount !== 1 ? "s" : ""}
                </span>

            </div>

            <div class="workshop-card-footer">

                <a
                    href="#/workshops/${workshop.id}"
                    class="button"
                >
                    Ver detalhes
                </a>

                <div class="card-actions">

                    <button
                        type="button"
                        class="icon-button edit-button"
                        data-action="edit"
                        data-id="${workshop.id}"
                        aria-label="Editar ${workshop.nome}"
                        title="Editar workshop"
                    >
                        ✎
                    </button>

                    <button
                        type="button"
                        class="icon-button delete-button"
                        data-action="delete"
                        data-id="${workshop.id}"
                        aria-label="Excluir ${workshop.nome}"
                        title="Excluir workshop"
                    >
                        🗑
                    </button>

                </div>

            </div>

        </article>
    `;
}

function formatDate(date) {
    return new Date(`${date}T00:00:00`)
        .toLocaleDateString("pt-BR");
}

export {
    renderWorkshopCard
};