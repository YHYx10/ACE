<template>
  <div class="category">
    <span
      class="category__header"
      :class="{ active: opens }"
      @click="toogleOpen"
    >
      <span>{{ loc(category.title) }}</span>
      <img src="/img/tattooShop/arrow-item.svg" alt="" />
    </span>
    <div class="category__list">
      <categories-item
        v-for="(item, index) in category.items"
        :key="index"
        :item="item"
        @onItemClick="setCurrentCategory"
      />
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";
import CategoriesItem from "./CategoriesItem";

export default {
  name: "CategoriesBlock",
  data() {
    return {
      opens: false,
    };
  },

  components: { CategoriesItem },
  methods: {
    setCurrentCategory(item) {
      const config = this.tattoos[this.category.key];
      if (config == undefined) return;
      const tattoos = [];
      config.forEach((c) => {
        let check = false;
        item.ids.forEach((id) => {
          if (this.gender ? c.Male !== "" : c.Female !== "") {
            if (c.Slots.indexOf(id) !== -1) {
              check = true;
              return;
            }
          }
        });

        if (check) tattoos.push(c);
      });
      window.console.log(item.ids[0]);
      this.$emit(
        "onSelectCtegory",
        this.category,
        tattoos,
        this.cameraPosition(item.ids[0])
      );
    },

    open() {
      this.$el.querySelector(".category__list").style.height =
        this.$el.querySelector(".category__list").scrollHeight + "px";
      this.opens = true;
    },

    hide() {
      this.$el.querySelector(".category__list").style.height = 0 + "px";
      this.opens = false;
    },

    toogleOpen() {
      this.opens ? this.hide() : this.open();
      this.$emit("change-open", this.opens);
    },
  },
  props: {
    category: Object,
  },

  computed: {
    ...mapState("tattooShop", ["tattoos", "gender"]),
    ...mapGetters("localization", ["loc"]),
    ...mapGetters("tattooShop", ["cameraPosition"]),
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.category {
  width: conv(350);
  height: auto;
  min-height: conv(88);
  overflow: hidden;

  &__header {
    border: 1px solid rgba($color: white, $alpha: 0.09);
    background: rgba(0, 0, 0, 0.2);
    min-height: conv(88);
    display: flex;
    width: 100%;
    flex-direction: row;
    align-items: center;
    justify-content: space-between;
    padding-left: conv(36);
    padding-right: conv(20);
    position: relative;
    cursor: pointer;
    transition: 0.2s ease;

    &::after {
      content: "";
      position: absolute;
      top: 50%;
      left: conv(24);
      height: conv(27);
      width: conv(2);
      background: #ffffff;
      transform: translateY(-50%);
      box-shadow: 0px 0px conv(14.6364) rgba(255, 255, 255, 0.55);
    }

    span {
      font-weight: 700;
      font-size: conv(24);
      line-height: conv(29);
      text-transform: uppercase;
      color: #ffffff;
    }

    img {
      height: conv(40);
      width: conv(40);
      display: none;
    }

    &:hover {
      background: rgba(0, 0, 0, 0.5);

      img {
        display: block;
      }
    }

    &.active {
      background: rgba(0, 0, 0, 0.5);

      img {
        display: block;
        transform: rotate(180deg);
      }
    }
  }

  &__list {
    transition: 0.3s ease;
    height: 0px;
    width: 100%;
    display: flex;
    flex-direction: column;
  }

  &:not(:last-child) {
    margin-bottom: conv(10);
  }
}
/* .category {
  margin: 1.6rem 0 0 0;
  &:first-child {
    margin: 0;
  }
  &__header {
    text-transform: uppercase;
    font-size: 1.5rem;
    color: #fff;
  }
  &__list {
    display: flex;
    flex-direction: column;
  }
} */
</style>
