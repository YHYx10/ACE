<template>
  <div class="categories">
    <div class="category_list">
      <div
        v-for="(category, index) of list"
        :key="index"
        class="item"
        :class="{ active: index === currentIndex }"
        @click="set(index)"
      >
        <img
          :src="category.imgPath"
          :alt="category"
          class="item_icon"
        />
      </div>
    </div>
    <div class="title">
      <span> The category is selected. </span>
      <span class="value">{{ loc(currentTitle) }}</span>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'SelectCategory',

  props: {
    // category: Object,
    list: Array,
    currentIndex: Number,
    currentTitle: String,
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
      this.$emit('setCurrent', item)
    },
  },
}
</script>

<style lang="scss" scoped>
.category_list {
  color: white;
  display: flex;
  gap: 0.741vh;
}
.item {
  display: flex;
  justify-content: center;
  align-items: center;
  border: 0.052vw solid rgba(255, 255, 255, 0.13);
  width: 4.815vh;
  height: 4.815vh;
  .item_icon {
    opacity: 0.5;
    filter: invert(0.5);
    transform: scale(0.75);
  }
  &:hover {
    border: 0.052vw solid rgba(255, 255, 255, 0.9);
  }
  &.active {
    border: 0.052vw solid rgba(255, 255, 255, 0.9);
    background-image: url('../UI/components/img/highlight.svg');
    background-repeat: no-repeat;
    background-size: cover;
    .item_icon {
      opacity: 1;
      filter: invert(0);
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
  .value {
    background: linear-gradient(
      180deg,
      #301934  0%,
      #ea0505 49.48%,
      #301934  100%
    );
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    background-clip: text;
  }
}
</style>
