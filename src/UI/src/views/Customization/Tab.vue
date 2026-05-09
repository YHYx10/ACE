<template>
  <ul class="tabs__list">
    <li
      class="tabs__item"
      v-for="({ img, imgSize }, index) in menu"
      :key="index"
    >
      <button
        class="tabs__btn"
        :class="{ 'tabs__btn--active': index === current }"
        :style="{
          backgroundImage: `url('/img/customization/${img}.png')`,
          backgroundSize: `${imgSize}px`,
        }"
        @click="() => $emit('change', index)"
      ></button>
    </li>
  </ul>
</template>

<script>
export default {
  data() {
    return {
      menu: [
        {
          img: "info-ico",
          imgSize: 11,
        },
        {
          img: "family-ico",
          imgSize: 25,
        },
        {
          img: "genetic-ico",
          imgSize: 25,
        },
        {
          img: "hair-ico",
          imgSize: 24,
        },
        {
          img: "brush-ico",
          imgSize: 24,
        },
        {
          img: "tr-ico",
          imgSize: 24,
        },
        {
          img: "clothes-ico",
          imgSize: 23,
        },
      ],
    };
  },
  props: {
    current: Number,
  },
};
</script>