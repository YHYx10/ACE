<template>
  <div class="categories">
    <div class="category_list">
      <div 
        v-for="(category, index) of categories" 
        :key="index" class="item" 
        :class="{active: category.type === currentType}"
        @click="set(category)"
      >
      <img
      :src="`/img/barbershop/${category.type}.svg`"
      :alt="category.type"
      class="item_icon"
    >
      </div>
    </div>
    <div class="title">
      <span>Die Kategorie wird ausgewählt - </span>
      <span class="value">{{ loc(currentTitle) }}</span>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'SelectCategory',

  props: {
    category: Object,
    categories: Array,
    currentType: String,
    currentTitle: String
  },
  data: function() {
    return {
      selectedCategory: 0,
    }
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    set: function(item) {
      this.$emit('setCurrentCategory', item)
    },
  },
}
</script>

<style lang="scss" scoped>
.category_list {
  color: white;
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  grid-column-gap: 1.66vh;
  grid-row-gap: 1.48vh;
  width: min-content;
}
.item {
  display: flex;
  justify-content: center;
  align-items: center;
  border: 0.052vw solid rgba(255, 255, 255, 0.13);
  width: 6.67vh;
  height: 6.67vh;
  &:hover {
    border: 0.052vw solid rgba(255, 255, 255, .9);
  }
  &.active{
    border: 0.052vw solid rgba(255, 255, 255, .9);
    background-image: url('../assets/icons/highlight.svg');
    background-repeat: no-repeat;
    background-size: cover;
    .item_icon {
      filter: invert(1);
    }
  }
  .item_icon {
    width: 4.44vh;
    height: 4.44vh;
  }
}
.title {
  margin-top: 1.48vh;
  color: white;
  font-family: 'Montserrat';
  font-style: normal;
  font-weight: 600;
  font-size: 1.66vh;
  line-height: 2.03vh;
  color: #ffffff;
  .value{
    background: linear-gradient(180deg, #301934  0%, #EA0505 49.48%, #301934  100%);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    background-clip: text;
  }
}

</style>
