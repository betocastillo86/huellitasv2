var app = app || {};
app.Settings = {
    general: {
        pageSize: 10
    },
    customTables: {
        animalSubtype: 1,
        animalSize: 2,
        animalGenre: 3
    },
    statusTypes: {
        created: 0,
        published: 1,
        hidden: 2,
        closed: 3
    },
    contentRelationTypes: {
        parent: 'Parent',
        shelter : 'Shelter'
    },
    resources: {
        'UserRole.Public': 'Público',
        'UserRole.SuperAdmin': 'Super Administrador'
    }
}