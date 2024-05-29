const baseUrl = 'https://localhost:44383/api/EasterEggs';

Vue.createApp({
    data() {
        return {
            eggs: [],
            stockLevel: 0,
            selectedEgg: null,
            errorMessage: ''
        };
    },
    created() {
        this.getEggs();
    },
    methods: {
        getEggs() {
            axios.get(baseUrl)
                .then(response => {
                    this.eggs = response.data;
                })
                .catch(error => {
                    console.error(error);
                    this.errorMessage = 'Failed to retrieve Easter eggs';
                });
        },
        getStockLevel(egg) {
            this.selectedEgg = egg;
            axios.get(`${baseUrl}/stock/${egg.productNo}`) // Use productNo instead of id
                .then(response => {
                    this.stockLevel = response.data;
                })
                .catch(error => {
                    console.error(error);
                    this.errorMessage = 'Failed to retrieve stock level';
                });
        },
        addStock() {
            if (!this.selectedEgg) {
                this.errorMessage = 'No egg selected';
                return;
            }

            axios.put(`${baseUrl}/stock/${this.selectedEgg.productNo}`, { stock: this.stockLevel }) // Use productNo instead of id
                .then(response => {
                    this.getEggs();
                    this.stockLevel = 0;
                })
                .catch(error => {
                    console.error(error);
                    this.errorMessage = 'Failed to update stock level';
                });
        }
    }
}).mount('#app');
