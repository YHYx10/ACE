<template>
    <div class="modal">

        <MainObjectCard class=""/>
        <div class="categories-object">
            <div class="category-object" v-for="item in categoryItems" :key="item.id" @click.native="addBorder">
                <CategoriesObjectCard :category="item" @modalClickCategory='addBorderCategory' class="object-modal-item" :class="{active: item.id === currentCategoryId}"/>
            </div>

        </div>
        <div class="object-items">
            <div class="object-item" v-for="item in filteredItems" :key="item.id">
                <ItemObjectCard :objectCard="item" @modalClick='addBorderItem' class="object-modal-item" />
            </div>
        </div>


    </div>
</template>

<script>
import MainObjectCard from './commons/MainObjectCard.vue'
import CategoriesObjectCard from './commons/CategoriesObjectCard.vue';
import ItemObjectCard from './commons/ItemObjectCard.vue';
export default {
    name: 'ObjectModal',
    components: {
        MainObjectCard,
        CategoriesObjectCard,
        ItemObjectCard
    },
    computed: {
        filteredItems() {
            return this.objectItems.filter(item => item.category === this.currentCategoryId)
        }
    },
    data() {
        return {
            activeELItem: null,
            activeElCategory: null,
            currentCategoryId: 0,
            categoryItems: [
                { id: 0, text: 'cloth 1', key: '1' },
                { id: 1, text: 'cloth 2', key: '2' },
                { id: 2, text: 'cloth 3', key: '3' },
                { id: 3, text: 'cloth 4', key: '4' },
                { id: 4, text: 'cloth 5', key: '5' },
                { id: 5, text: 'cloth 6', key: '6' },
                { id: 6, text: 'cloth 7', key: '7' },
            ],
            objectItems: [
                {
                    id: 0, text: 'GUCCI Polluted- Bill', category: 0
                },
                { id: 1, text: 'GUCCI Polluted- Bill', category: 1 },
                { id: 2, text: 'GUCCI Polluted- Bill', category: 0 },
                { id: 3, text: 'GUCCI Polluted- Bill', category: 0 },
                { id: 4, text: 'GUCCI Polluted- Bill', category: 0 },
                { id: 5, text: 'GUCCI Polluted- Bill', category: 0 },
            ]
        }
    },
    methods: {
        addBorderItem(id) {
            if (this.activeELItem == null) {
                console.log('asd')
                document.getElementById(id).classList.add('active')
                this.activeELItem = id
            } else {
                document.getElementById(this.activeELItem).classList.remove('active')
                this.activeELItem = id
                document.getElementById(this.activeELItem).classList.add('active')
            }
        },
        addBorderCategory(id) {
            this.currentCategoryId = id
        }
    }
}
</script>

<style lang="scss" scoped>
.modal {
    position: absolute;
    display: flex;
    top: -0.09rem;
    left: 0;
    width: 100%;
    height: 100%;
    z-index: 100;
    // border: 1.112px solid white;
    // background: url("/img/optionsMenu/bg.png"), rgba(0, 0, 0, 0.96);
    background-blend-mode: overlay;
}

.active {
    // border: 1.112px solid #5CFF80 !important;
}

.category-object {
    margin-bottom: 1.1295rem;

}

.categories-object {
    display: flex;
    flex-direction: column;
    margin-right: 1.8494rem;
}
.object-modal-no-border {
  //transition: background-color 0.4s ease;

  &:hover {
    //transform: scale(1.04);
    background-color: #1E1E1E;

  }
}
.object-items {
    display: flex;
    width: 54.893rem;
    max-height: 41.697rem;
    flex-wrap: wrap;
    justify-content: space-between;
}
.object-modal-item {
  //transition: border 0.4s ease;

  &:hover {
    //transform: scale(1.04);
    // border: 1.112px solid #5CFF80 !important;


  }
}
</style>