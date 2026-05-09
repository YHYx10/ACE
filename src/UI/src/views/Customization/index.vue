<template>
  <div class="customization">
    <span class="title">Creation of a character</span>
    <p class="descr">Here you can configure the appearance of the future character</p>
    <div class="tabs">
      <Tab @change="selectCategory" :current="currentCategory" />
      <component
        :is="currentTab"
        :itemData="itemData"
        @onSelectSubcategory="selectSubcategory"
        :subcategory="currentItem"
      />
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";
import Info from "./tabs/Info.vue";
import Parents from "./tabs/Parents.vue";
import Dna from "./tabs/Dna.vue";
import Hairs from "./tabs/Hairs.vue";
import Skin from "./tabs/Skin.vue";
import Clothes from "./tabs/Clothes.vue";
import Makeup from "./tabs/Makeup.vue";
import Tab from "./Tab.vue";

export default {
  data() {
    return {
      currentCategory: 0,
      currentItem: 0,
    };
  },
  components: {
    Info,
    Parents,
    Dna,
    Hairs,
    Skin,
    Clothes,
    Makeup,
    Tab,
  },
  computed: {
    ...mapGetters("customization", ["categories"]),
    ...mapState("customization", ["gender"]),
    ...mapGetters("localization", ["loc"]),
    currentTab() {
      return this.categories[this.currentCategory].tab;
    },
    items() {
      return this.categories[this.currentCategory].items;
    },
    itemData() {
      if (this.items == undefined) return null;
      return this.currentCategory === 2
        ? this.items
            .map((elem) => {
              console.log(elem.itemData);
              return elem.itemData;
            })
            .flat()
        : this.items[this.currentItem].itemData;
    },
  },
  methods: {
    selectRandom() {
      this.randomParents();
      this.randomDna();
      this.randomHairs();
    },
    selectCategory(index) {
      if (index === this.currentCategory) return;
      this.currentItem = 0;
      this.currentCategory = index;
      window.mp.trigger("customization:camera:switch", index === 6);
    },
    randomParents() {
      const parents = this.categories[1];
      if (parents !== undefined) {
        parents.items.forEach((parent) => {
          parent.itemData.value = Math.floor(
            Math.random() * parent.itemData.items.length
          );
          window.mp.trigger(
            "customization:update",
            parent.itemData.tag,
            parent.itemData.items[parent.itemData.value]
          );
        });
      }
    },
    selectSubcategory(index) {
      this.currentItem = index;
    },
    randomDna() {
      const dna = this.categories[2];
      if (dna !== undefined) {
        dna.items.forEach((d) => {
          d.itemData.forEach((i) => {
            const range = [];
            for (let index = i.min; index <= i.max; index += i.step) {
              range.push(index);
            }
            i.value = range[Math.floor(Math.random() * range.length)];
            window.mp.trigger("customization:update", i.tag, i.value);
          });
        });
      }
    },
    randomHairs() {
      const hairs = this.categories[3];
      if (hairs !== undefined) {
        hairs.items.forEach((h) => {
          if (!this.gender && !h.itemData.randomFemale) h.itemData.value = -1;
          else
            h.itemData.value = Math.floor(
              Math.random() * h.itemData.items.length
            );
          window.mp.trigger(
            "customization:update",
            h.itemData.tag,
            h.itemData.items[h.itemData.value]
          );
        });
      }
    },
  },
};
</script>

<style lang="scss">
@font-face {
  font-family: "Akrobat", sans-serif;
  src: url("/fonts/Akrobatbold.woff2") format("woff2");
  font-display: swap;
  font-weight: 700;
  font-style: normal;
}

@font-face {
  font-family: "Akrobat";
  src: url("/fonts/Akrobatextrabold.woff2") format("woff2");
  font-display: swap;
  font-weight: 800;
  font-style: normal;
}

@font-face {
  font-family: "Akrobat";
  src: url("/fonts/Akrobatsemibold.woff2") format("woff2");
  font-display: swap;
  font-weight: 600;
  font-style: normal;
}

.customization {
  margin: 0;
  font-family: "Akrobat";
  overflow-anchor: none;
  background-image: url("/img/customization/bg-home.jpg");
  background-size: cover;
  background-repeat: no-repeat;
  background-position: center center;
  width: 100vw;
  height: 100vh;
  background: linear-gradient(
    270deg,
    rgba(0, 0, 0, 0) 0%,
    rgba(0, 0, 0, 0) 44.4%,
    rgba(1, 1, 1, 0.85) 72.06%,
    #010101 100%
  );

  .customization-nav-range_value {
    padding: 2.706350914962325vh 1.7222820236813778vh 3.013993541442411vh
      3.567491926803014vh;
  }
  .customiztion-nav-range .vue-slider-rail {
    width: 19.91388589881593vh;
    margin-right: 1.6146393972012918vh;
  }

  a {
    text-decoration: none;
    color: inherit;
  }

  ul {
    list-style-type: none;
  }

  .btn {
    cursor: pointer;

    &--save {
      text-align: center;
      border: none;
      font-family: "Akrobat";
      font-weight: 600;
      font-size: 1.5069967707212055vh;
      line-height: 1.8299246501614639vh;
      padding: 2.183423035522067vh 0 2.1910656620021527vh 0;
      width: 100%;
      text-transform: uppercase;
      color: #ffffff;
    border: 0.093vmin solid rgba($color: #301934 , $alpha: 1);
      background: linear-gradient(
        rgba($color: #301934 , $alpha: 0.25),
        rgba($color: #591b87, $alpha: 0.25)
      );
      transition: 0.3s ease;
      box-shadow: inset 0 0 1.389vmin rgba($color: #301934 , $alpha: 0.86);

      &:hover {
        transition: 0.3s ease;
        box-shadow: inset 0vh 0vh 13.889vh #301934 ;
        filter: drop-shadow(0vh 0vh conv(15) rgba(71, 44, 132, 0.5));
      }
    }
  }

  // .btn:hover {
  //   transition: background-color 0.3s ease-in-out;
  // }

  .btn:disabled {
    opacity: 0.4;
    pointer-events: none;
  }

  .title {
    font-weight: 800;
    font-size: 5.166846071044134vh;
    line-height: 5.243272vh;
    background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    background-clip: text;
    text-fill-color: transparent;
    text-shadow: 0 0 12.055974165769644vh rgba(255, 255, 255, 0.25);
    padding-left: 5.166846071044134vh;
  }

  .descr {
    color: #fff;
    font-weight: 600;
    font-size: 1.6146393972012918vh;
    line-height: 1.2375672766415502vh;
    text-transform: uppercase;
    padding-left: 5.166846071044134vh;
    margin-bottom: 1.2146393972012918vh;
  }

  .tabs {
    height: 92%;
    max-height: 92%;
    padding-left: 5.166846071044134vh;

    &__list {
      list-style-type: none;
      padding: 0;
      margin: 0;
      display: flex;
      align-items: center;
    }

    &__btn {
      border: none;
      display: flex;
      align-items: center;
      justify-content: center;
      font-family: "Akrobat";
      font-weight: 700;
      background: rgba(255, 255, 255, 0.01);
      border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
      border-radius: 0.21528525296017223vh;
      width: 5.920344456404736vh;
      height: 5.920344456404736vh;
      cursor: pointer;
      margin-right: 1.0764262648008611vh;
      background-repeat: no-repeat;
      background-position: center center;
      transition: background-color 0.3s ease-in-out, color 0.3s ease-in-out;

      &:hover {
        background-color: #09090a;
        color: rgb(236, 236, 236);
      }

      img {
        max-width: 2.9063509149623252vh;
        max-height: 3.013993541442411vh;
      }
    }

    &__btn--active {
      background: #301934 ;
      border: 0.10764262648008611vh solid #a7151c;
      transition: background-color 0.3s ease-in-out, color 0.3s ease-in-out;
      pointer-events: none;
      background-repeat: no-repeat;
      background-position: center center;
    }

    &__content {
      display: flex;
      height: 94%;
      max-height: 94%;
      flex-direction: column;
      justify-content: flex-end;
      overflow: hidden;
    }
  }

  .form {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 100%;
    &__body {
      display: flex;
      flex-direction: column;
      max-height: 88%;
      overflow: hidden;
      overflow-x: hidden;
      overflow-y: auto;
      width: 103%;

      &::-webkit-scrollbar {
        width: 0.2rem;
      }
      &::-webkit-scrollbar-track {
        border: solid 0 transparent;
        margin: 0;
      }
      &::-webkit-scrollbar-thumb {
        border: solid 0 transparent;
        background-color: #301934 ;
      }
    }
  }

  .content {
    padding: 0.7764262648008611vh 0 0 0;
    width: 47.90096878363832vh;
    color: #fff;
    height: 100%;
  }

  .form {
    &__sex {
      display: flex;
      align-items: center;
      margin-bottom: 0.6534983853606028vh;
    }

    &__name {
      display: flex;
      align-items: center;
      margin-bottom: 0.6534983853606028vh;
    }

    &__subname {
      display: flex;
      align-items: center;
      margin-bottom: 0.6534983853606028vh;
    }

    &__parents {
      display: flex;
      margin-bottom: 0.6534983853606028vh;

      &_select {
        margin-right: 1.2917115177610334vh;
      }

      &_content {
        height: 29vh;
        align-content: flex-start;
      }
    }

    &__face {
      display: flex;
      align-items: center;
      margin-bottom: 0.6534983853606028vh;
    }

    &__leather {
      display: flex;
      align-items: center;
      margin-bottom: 0.6534983853606028vh;
    }

    &__color {
      display: flex;
      align-items: center;
      margin-bottom: 0.6534983853606028vh;
    }

    &__selector {
      display: grid;
      justify-content: center;
      align-items: center;
      grid-template-columns: 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh;
      column-gap: 0.5974165769644779vh;
      row-gap: 0.5382131324004306vh;
      background-image: url("/img/customization/bg-input.png");
      padding: 1.1840688912809472vh 1.566200215285253vh 1.1356297093649086vh
        1.1840688912809472vh;
      width: 29.278794402583422vh;
      height: 7.750269106566201vh;
      border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
    }

    &__value {
      display: flex;
      align-items: center;
      background-image: url("/img/customization/bg-input.png");
      padding: 2.7063509149623252vh 1.7222820236813778vh 3.013993541442411vh
        3.567491926803014vh;
      border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
      width: 29.378794402583422vh;
      height: 7.755vh;
    }

    &__input {
      font-family: "Akrobat";
      width: 29.278794402583422vh;
      height: 7.750269106566201vh;
      padding: 2.9063509149623252vh 0 3.013993541442411vh 0;
      border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
      background: url("/img/customization/bg-input.png");
      text-align: center;
      background-size: cover;
      color: #fff;
      font-weight: 600;
      font-size: 1.5069967707212055vh;
      line-height: 1.8299246501614639vh;

      &::placeholder {
        color: inherit;
        font-weight: inherit;
        font-size: inherit;
        line-height: inherit;
      }
    }

    &__box {
      border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
      background-image: url("/img/customization/bg-select.png");
      padding: 2.3681377825618943vh 0 2.3681377825618943vh 3.552206673842842vh;
      width: 17.222820236813778vh;
      height: 7.750269106566201vh;
      color: #fff;
      font-weight: 700;
      font-size: 1.2917115177610334vh;
      line-height: 1.5069967707212055vh;
      text-transform: uppercase;
      margin-right: 1.2917115177610334vh;
      position: relative;

      &::after {
        content: "";
        position: absolute;
        width: 2.798708288482239vh;
        height: 0.21528525296017223vh;
        top: 50%;
        left: 0;
        background: #ffffff;
        box-shadow: 0 0 1.5069967707212055vh rgba(255, 255, 255, 0.55);
        transform: rotate(-90deg);
      }

      h3 {
        font-family: "Akrobat";
        color: #fff;
        font-weight: 700;
        font-size: 1.2917115177610334vh;
        line-height: 1.5069967707212055vh;
        max-width: 7.35629709364909vh;
      }
    }
  }

  .color + label {
    margin: 0;
    border: none;
    width: 2.416576964477933vh;
    height: 2.416576964477933vh;
    cursor: pointer;
    transition: 0.3s all;
  }

  .selected + label {
    transition: 0.3s all;
    border: 0.25764262648008611vh solid #000000;
  }

  input[id="color-1"] + label {
    background: #fff;
  }

  input[id="color-2"] + label {
    background: #ff8f8f;
  }

  input[id="color-3"] + label {
    background: #ff2323;
  }

  input[id="color-4"] + label {
    background: #ffcd4d;
  }

  input[id="color-5"] + label {
    background: #7bff4d;
  }

  input[id="color-6"] + label {
    background: #4dff74;
  }

  input[id="color-7"] + label {
    background: #4dffd4;
  }

  input[id="color-8"] + label {
    background: #4d69ff;
  }

  input[id="color-9"] + label {
    background: #9b4dff;
  }

  input[id="color-10"] + label {
    background: #0c0c0c;
  }

  input[id="color-11"] + label {
    background: #ff8717;
  }

  input[id="color-12"] + label {
    background: #fff617;
  }

  input[id="color-13"] + label {
    background: #61ff17;
  }

  input[id="color-14"] + label {
    background: #17f1ff;
  }

  input[id="color-15"] + label {
    background: #172fff;
  }

  input[id="color-16"] + label {
    background: #6117ff;
  }

  input[id="color-17"] + label {
    background: #d117ff;
  }

  input[id="color-18"] + label {
    background: #ff175d;
  }

  input[type="radio"] {
    display: none;
    transition: 0.3s all;
  }

  input[type="radio"]:checked + label {
    color: #00ff38;
  }

  .sex:checked + label {
    position: relative;

    &::after {
      content: "";
      position: absolute;
      bottom: 0.6458557588805167vh;
      left: 50%;
      transform: translateX(-50%);
      width: 6.027987082884822vh;
      height: 0.21528525296017223vh;
      background: #1aff4c;
      box-shadow: 0 -0.21528525296017223vh 2.2604951560818085vh #00ff38;
    }
  }

  .p-sex:checked + label {
    color: #5cff80 !important;
    border: 0.10764262648008611vh solid #5cff80;

    &::after {
      background: #5cff80;
      border: 0.10764262648008611vh solid rgba(92, 255, 128, 0.09);
      box-shadow: 0 0 1.5069967707212055vh rgba(92, 255, 128, 0.55);
      transform: rotate(-90deg);
    }
  }

  .parent-fa:checked + label {
    border: 0.10764262648008611vh solid #19ff4c;
  }

  label[id="male"],
  label[id="female"] {
    font-family: "Akrobat";
    color: #fff;
    font-weight: 600;
    height: 7.750269106566201vh;
    font-size: 1.5069967707212055vh;
    line-height: 1.8299246501614639vh;
    text-transform: uppercase;
    display: block;
    padding: 2.9063509149623252vh 4.090419806243272vh 3.013993541442411vh
      3.982777179763186vh;
    float: left;
    border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
    background-image: url("/img/customization/bg-select.png");
    cursor: pointer;
    transition: 0.3s all;
  }

  label h3 {
    font-family: "Akrobat";
    color: #fff;
    font-weight: 700;
    font-size: 1.2917115177610334vh;
  }

  label[id="parent-f1"],
  label[id="parent-f2"],
  label[id="parent-f3"],
  label[id="parent-f4"],
  label[id="parent-f5"],
  label[id="parent-f6"],
  label[id="parent-f7"],
  label[id="parent-f8"],
  label[id="parent-f9"] {
    overflow: hidden;
    width: 9.041980624327234vh;
    height: 9.041980624327234vh;
    display: flex;
    align-items: center;
    justify-content: center;
    border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
    padding: 0.8611410118406889vh 0.5382131324004306vh 0 0.43057050592034446vh;
    cursor: pointer;
    transition: 0.3s all;
  }

  label[id="p-male"],
  label[id="p-female"] {
    display: flex;
    color: #fff;
    font-weight: 600;
    width: 17.330462863293864vh;
    height: 7.750269106566201vh;
    font-size: 1.5069967707212055vh;
    line-height: 1.6299246501614639vh;
    text-transform: uppercase;
    padding: 2.3681377825618943vh 0 2.1681377825618943vh 3.552206673842842vh;
    border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
    background-image: url("/img/customization/bg-select.png");
    cursor: pointer;
    position: relative;
    transition: 0.3s all;

    &::after {
      content: "";
      position: absolute;
      width: 2.798708288482239vh;
      height: 0.21528525296017223vh;
      top: 50%;
      left: 0;
      background: #ffffff;
      box-shadow: 0 0 1.5069967707212055vh rgba(255, 255, 255, 0.55);
      transform: rotate(-90deg);
    }
  }

  label[id="male"] {
    margin-right: 1.2917115177610334vh;
  }

  label[id="p-male"] {
    margin-bottom: 1.0764262648008611vh;
  }

  label[id="male"]:hover,
  label[id="female"]:hover {
    color: #00ff38;
  }

  /* Ползунок */
  output {
    font-family: "Akrobat";
    color: #00ff38;
    text-transform: uppercase;
    font-weight: 700;
    font-size: 1.5069967707212055vh;
    line-height: 1.8299246501614639vh;
  }

  input[type="number"] {
    font-family: "Akrobat";
    width: 4.3057050592034445vh;
    padding: 0.43057050592034446vh 0.5382131324004306vh;
    border: 0.10764262648008611vh solid #bbb;
    border-radius: 0.32292787944025836vh;
  }

  input[type="range"] {
    font-family: "Akrobat";
    -webkit-appearance: none;
    margin-right: 1.6146393972012918vh;
    width: 19.91388589881593vh;
    height: 0.33057050592034446vh;
    background: rgba(255, 255, 255, 0.25);
    background-image: linear-gradient(#1aff4c, #1aff4c);
    background-size: 70% 100%;
    background-repeat: no-repeat;
  }

  /* Input Thumb */
  input[type="range"]::-webkit-slider-thumb {
    -webkit-appearance: none;
    height: 0.8611410118406889vh;
    width: 0.8611410118406889vh;
    background: #ffffff;
    cursor: ew-resize;
    box-shadow: 0 0 0.21528525296017223vh 0 #555;
    transition: background 0.3s ease-in-out;
  }

  input[type="range"]::-moz-range-thumb {
    -webkit-appearance: none;
    height: 0.8611410118406889vh;
    width: 0.8611410118406889vh;
    background: #ffffff;
    cursor: ew-resize;
    box-shadow: 0 0 0.21528525296017223vh 0 #555;
    transition: background 0.3s ease-in-out;
  }

  input[type="range"]::-ms-thumb {
    -webkit-appearance: none;
    height: 0.8611410118406889vh;
    width: 0.8611410118406889vh;
    background: #ffffff;
    cursor: ew-resize;
    box-shadow: 0 0 0.21528525296017223vh 0 #555;
    transition: background 0.3s ease-in-out;
  }

  input[type="range"]::-webkit-slider-thumb:hover {
    background: #ffffff;
  }

  input[type="range"]::-moz-range-thumb:hover {
    background: #ffffff;
  }

  input[type="range"]::-ms-thumb:hover {
    background: #ffffff;
  }

  /* Input Track */
  input[type="range"]::-webkit-slider-runnable-track {
    -webkit-appearance: none;
    box-shadow: none;
    border: none;
    background: transparent;
  }

  input[type="range"]::-moz-range-track {
    -webkit-appearance: none;
    box-shadow: none;
    border: none;
    background: transparent;
  }

  input[type="range"]::-ms-track {
    -webkit-appearance: none;
    box-shadow: none;
    border: none;
    background: transparent;
  }

  .p-img {
    width: 8.073196986006458vh;
    height: 8.073196986006458vh;
  }

  .form__parents_content {
    overflow-x: hidden;
    overflow-y: scroll;
    box-sizing: content-box;
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: flex-start;
    align-content: flex-start;
    margin-right: 2vh;

    div:not(:nth-child(3n)) {
      margin-right: 0.25vh;
    }
    div:not(:nth-last-child(-n + 3)) {
      margin-bottom: 0.6764262648008611vh;
    }

    &::-webkit-scrollbar {
      width: 0.2rem;
    }
    &::-webkit-scrollbar-track {
      border: solid 0 transparent;
      margin: 0;
    }
    &::-webkit-scrollbar-thumb {
      border: solid 0 transparent;
      background-color: #301934 ;
    }
  }

  .vue-slider-dot {
    display: flex;
    justify-content: center;
    align-items: center;
  }
}
</style>