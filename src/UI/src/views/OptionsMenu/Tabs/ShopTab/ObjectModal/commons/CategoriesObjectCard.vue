<template>
    <div class="categories-card" :id="category.id+'-category'" ref="categoryObject" @click="modalClick">
        <div class="background">
            <div class="background-1"></div>
            <div class="background-2" :style="`background-image: url(${require('../img/' + category.id + '.svg')});`">
                
            </div>
        </div>
        <div class="text-part">
            <span class="text-category">{{category.text}}</span>
        </div>
    </div>
</template>

<script>
export default {
    name: 'CategoriesObjectCard',
    props: {
        category: Object
    },
    methods: {
        modalClick() {
            this.$emit('modalClickCategory', this.category.id)
        }
    }
}

</script>

<style lang="scss" scoped>
.categories-card {
    width: 4.806rem;
    height: 4.806rem;
    border-radius: 1.35rem;
    border: 0.0495rem solid #1E1E1E;
    position: relative;
    overflow: hidden;
    display: flex;
    flex-direction: column;
    /*transition: transform 0.4s ease;

    &:hover {
        transform: scale(1.04);
        border: 1.112px solid #5CFF80 !important;


    }*/
    &:hover {
        background: rgba(255, 255, 255, 0.1);
    }

}

.active {
    background: rgba(255, 255, 255, 0.2);
    &:hover {
        background: rgba(255, 255, 255, 0.2);
    }
}

.background {
    position: absolute;
    width: 100%;
    height: 100%;

    &-1 {
        position: absolute;
        width: 9.495rem;
        height: 9.495rem;
        left: 3.699rem;
        top: 4.995rem;

        background: #FFFFFF;
        opacity: 0.25;
        // filter: blur(4.4496rem);
    }

    &-2 {
        background-position: center;
        background-size: contain;
        position: absolute;
        background-repeat: no-repeat;
        left: 50%;
        text-align: center;
        width: 50px;
        height: 50px;
        top: 40%;
        transform: translate(-50%, -50%);

    }
}

.text-part {
    margin: 0 auto;
    margin-top: 3.33rem;
}

.text-category {
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 0.594rem;
    line-height: 0.693rem;
    text-transform: uppercase;

    color: #FFFFFF;

}
</style>