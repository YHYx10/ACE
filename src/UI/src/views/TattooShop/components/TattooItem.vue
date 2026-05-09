<template>
  <div
    v-if="show"
    :class="[{ active: selected }, 'tattoo-item']"
    @click="onSelect()"
  >
    <div class="tattoo-item__header">
      <span class="title">{{ tattoo.Name }}</span>
      <span class="price"
        >{{
          Math.round((tattoo.Price / 100) * price).toLocaleString("en")
        }}$</span
      >
    </div>
    <img
      :src="`img/tattooShop/tattoo/${tattoo.Name.toLowerCase().replace(
        /\s/g,
        '_'
      )}.png`"
      :alt="tattoo.title"
      class="tattoo-item__img"
    />
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "TattooItem",
  computed: {
    ...mapState("tattooShop", ["price"]),
  },
  props: ["tattoo", "index", "selected", "show"],
  methods: {
    onSelect() {
      this.$emit("onTattooSelect", this.index);
    },
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.tattoo-item {
  padding: conv(8);
  width: 100%;
  height: conv(169);
  min-height: conv(169);
  position: relative;
  background: rgba(255, 255, 255, 0.05);
  border: 1px solid rgba($color: white, $alpha: 0.09);
  cursor: pointer;
  transition: 0.3s ease;

  &:hover {
    background: rgba(255, 255, 255, 0.13);
  }

  &.active {
    background: rgba(255, 255, 255, 0.3);
  }

  &__header {
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-between;

    .title {
      width: 100%;
      text-align: center;
      font-weight: 700;
      font-size: conv(16);
      line-height: conv(19);
      display: flex;
      align-items: center;
      justify-content: center;
      text-transform: uppercase;
      color: #ffffff;
    }

    .price {
      font-weight: 700;
      font-size: conv(20);
      line-height: conv(24);
      text-align: center;
      text-transform: uppercase;
      color: #a0ff98;
      width: 100%;
    }
  }

  img {
    position: absolute;
    top: conv(47);
    left: 50%;
    transform: translateX(-50%);
    height: conv(70);
  }
}

/* .tattoo-item {
  height: 9rem;
  background: rgba(255, 255, 255, 0.06);
  display: flex;
  flex-direction: column;
  align-items: center;
  border: 1px solid transparent;
  &.active {
    background: rgba(255, 255, 255, 0.18);
    border-color: rgba(193, 231, 4, 0.3);
    .tattoo-item__header {
      .title {
        font-weight: 500;
        color: #C1E704;
        letter-spacing: 0.05em;
      }
      .price {
        color: rgba(255, 255, 255, 0.75);
      }
    }
  }
  &__header {
    width: 100%;
    height: 2rem;
    display: flex;
    justify-content: space-between;
    flex-wrap: nowrap;
    padding: 0.8rem 0.4rem 0 0.4rem;
    color: rgba(255, 255, 255, 0.45);
    .title {
      font-weight: 300;
      width: 70%;
      transition: all 0.15s;
      font-size: 0.75rem;
    }
    .price {
      font-weight: 300;
      transition: all 0.15s;
      font-size: 0.75rem;
    }
  }
  &__img {
    width: 6.2rem;
    height: 6.2rem;
    margin: 0.25rem 0 0 0;
  }
} */
</style>
