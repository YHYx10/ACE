<template>
  <div class="tattoo-shop">
    <div class="tattoo-shop_left">
      <div class="tattoo-shop_header">
        <div class="tattoo-shop_title">
          TATTOO
          <span>Salon</span>
        </div>
        <div class="tattoo-shop_descr" v-if="tattooList.length == 0">
     Select the category
        </div>
        <button class="tattoo-shop_btn" v-else @click="back">
          <div>
            <img src="/img/inputMenu/arrow.svg" alt="" />
          </div>
          <span>BACK</span>
        </button>
      </div>
      <div
        class="tattoo-shop_main"
        :class="{
          tab: tattooList.length !== 0,
        }"
      >
        <transition name="tattoo-tab">
          <CategoriesTab
            @onSelectCtegory="selectCategory"
            v-if="tattooList.length == 0"
          />
          <TattoosTab
            @onBack="back"
            :items="tattooList"
            :category="currentCategory.key"
            v-else
          />
        </transition>
      </div>
    </div>

    <div class="tattoo-shop_right">
      <ExitCross class="close" @click="close" />
    </div>
  </div>
  <!-- <div class="tattoo-shop">
    <div class="substrate substrate-top"></div>
    <div class="substrate substrate-bottom"></div>
    <div class="tattoo-shop__header">
      <div class="desc">{{ loc("tattoo_shop_1") }}</div>
      <div class="title">{{ loc(currentCategory.title || defaultTittle) }}</div>
    </div>
    <div class="tattoo-shop__main">
      <transition name="tattoo-tab">
        <CategoriesTab
          @onSelectCtegory="selectCategory"
          v-if="tattooList.length == 0"
        />
        <TattoosTab
          @onBack="back"
          :items="tattooList"
          :category="currentCategory.key"
          v-else
        />
      </transition>
    </div>
  </div> -->
</template>

<script>
import { mapState, mapGetters } from "vuex";
import ExitCross from '../UI/components/ExitCross.vue';
import CategoriesTab from "./components/CategoriesTab";
import TattoosTab from "./components/TattoosTab";

export default {
  name: "TattooShop",

  components: { CategoriesTab, TattoosTab, ExitCross },

  data: function () {
    return {
      tattooList: [],
      currentCategory: {},
      defaultTittle: "tattoo_shop_16",
    };
  },

  methods: {
    selectCategory(category, tattoos) {
      //window.console.log(category, tattoos)
      this.currentCategory = category;
      this.tattooList = tattoos;
    },
    back() {
      this.tattooList = [];
      this.currentCategory = {};
    },

    close() {
      window.mp.trigger("tattoo:close");
    },
  },

  computed: {
    ...mapState("tattooShop", ["tattoos", "gender"]),
    ...mapGetters("localization", ["loc"]),
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

@import url("https://fonts.googleapis.com/css2?family=Roboto:wght@300;500&display=swap");

@keyframes fade-in {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

.tattoo-shop {
  width: 100vw;
  height: 100vh;
  position: relative;
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  padding: conv(40) conv(40) 0 conv(25);

  * {
    font-family: "Akrobat";
  }

  &::after {
    content: "";
    position: absolute;
    z-index: -1;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: radial-gradient(
      50% 94.42% at 50% 50%,
      rgba(0, 0, 0, 0) 0%,
      #000000 100%
    );
    opacity: 0.8;
  }

  &_left {
    padding-top: conv(66);
    display: flex;
    flex-direction: column;
  }

  &_header {
    padding-left: conv(15);
  }

  &_title {
    display: flex;
    align-items: center;
    font-weight: 800;
    font-size: conv(55);
    line-height: conv(76);
    color: #ffffff;

    span {
      display: block;
      background: linear-gradient(
        180deg,
        #301934  0%,
        #ea0505 49.48%,
        #301934  100%
      );
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      text-fill-color: transparent;
      text-shadow: 0px 0px conv(49) rgba(255, 0, 0, 0.52);
    }
  }

  &_descr {
    margin-top: conv(5);
    font-weight: 700;
    font-size: conv(24);
    line-height: conv(29);
    color: #ffffff;
    margin-bottom: conv(36);
  }

  &_btn {
    margin-top: conv(9);
    margin-bottom: conv(30);
    display: flex;
    align-items: center;
    height: conv(47);
    background: rgba(255, 255, 255, 0.1);
    cursor: pointer;
    transition: 0.2s ease;

    &:hover {
      background: rgba(255, 255, 255, 0.2);
    }

    div {
      display: flex;
      align-items: center;
      justify-content: center;
      height: 100%;
      width: conv(47);
      min-width: conv(47);
      background: rgba(255, 255, 255, 0.1);

      img {
        height: conv(14);
      }
    }

    span {
      font-weight: 700;
      font-size: conv(20);
      line-height: conv(24);
      color: #ffffff;
      display: flex;
      align-items: center;
      justify-content: center;
      width: conv(99);
    }
  }

  &_main {
    display: flex;

    &:not(.tab) > div,
    .tattoos-tab__list {
      margin-right: conv(-10);
      padding-left: conv(10);
    }

    &:not(.tab) {
      max-height: conv(744);
    }

    &.tab {
      .tattoos-tab__list {
        max-height: conv(527);
      }
    }

    & > div,
    .tattoos-tab__list {
      overflow: auto;
      direction: rtl;

      &::-webkit-scrollbar {
        width: conv(3);
      }
      &::-webkit-scrollbar-track {
        background: rgba(255, 255, 255, 0.23);
      }
      &::-webkit-scrollbar-thumb {
        background: #ff0000;
      }
    }

    & > div > * {
      direction: ltr;
    }
  }

  &_right {
    button {
      width: conv(50);
      height: conv(50);
      display: flex;
      align-items: center;
      justify-content: center;
      cursor: pointer;
      background: none;
      border: none;
      outline: none;

      img {
        height: conv(30);
      }
    }
  }
}

/* .tattoo-shop {
  display: flex;
  flex-flow: column;
  align-items: flex-start;
  width: 100%;
  height: 100%;
  padding: 3.4rem 0 3.4rem 3.2rem;
  background: radial-gradient(
    55.56% 52.59% at 50% 47.41%,
    rgba(0, 0, 0, 0) 0%,
    rgba(0, 0, 0, 0.7) 100%
  );
  font-family: "Roboto", sans-serif;
  :lang(ge) {
    font-family: "BPG Arial Caps", sans-serif !important;
  }
  .substrate {
    content: "";
    position: absolute;
    width: 58rem;
    height: 58rem;
    background: rgba(193, 231, 4, 0.5);
    opacity: 0.4;
    filter: blur(4.5rem);
    z-index: 0;
    border-radius: 50%;
    &-top {
      left: -32.5rem;
      top: -40rem;
    }
    &-bottom {
      left: -30rem;
      bottom: -44.5rem;
    }
  }
  &__header {
    display: flex;
    flex-flow: column;
    position: relative;
    z-index: 2;
    &:after {
      content: "";
      position: absolute;
      height: 1px;
      width: 15rem;
      bottom: 0;
      left: 0;
      background: linear-gradient(
        90.01deg,
        #c1e704 0%,
        rgba(255, 255, 255, 0) 85.03%
      );
      border-radius: 0.25rem;
    }
    .desc,
    .title {
      text-transform: uppercase;
      color: #ffffff;
    }
    .desc {
      font-weight: normal;
      font-size: 1.5rem;
      line-height: 1.5rem;
    }
    .title {
      font-weight: bold;
      font-size: 4rem;
      line-height: 4rem;
      display: inline-block;
    }
  }
  &__main {
    z-index: 2;
    position: relative;
  }
} */
.tattoo-tab-enter-active,
.tattoo-tab-leave-active {
  transition: all 0.3s;
}
.tattoo-tab-enter,
.tattoo-tab-leave-to {
  opacity: 0;
  position: absolute;
}
.tattoo-tab-enter {
  transform: translate(-100%, 0);
  z-index: 2;
}
</style>
