<template>
  <div class="dressing-up-menu">
    <div class="dressing-up-menu_wrap">
      <div class="title">{{ loc("dress:up:menu:1") }}</div>
      <!-- <div class="descr">{{ loc("dress:up:menu:2") }}</div> -->
      <div class="list">
        <div>
          <Dress
            v-for="dress in dresses"
            :key="dress.id"
            :dress="dress"
            :currentDressId="currentDressId"
            :setCurrentDressId="setCurrentDressId"
          />
        </div>
      </div>
      <div class="btns-wrap">
        <div class="btn" @click="toApply">{{ loc("dress:up:menu:3") }}</div>
        <div class="btn" @click="toCancel">{{ loc("dress:up:menu:4") }}</div>
      </div>
    </div>
    <ExitCross class="exit-cross" @click="toCancel" />
  </div>
</template>

<script>
import Dress from "./components/Dress";
import { mapGetters, mapState } from "vuex";
import ExitCross from '../UI/components/ExitCross.vue';

export default {
  name: "DressingUpMenu",

  data: function () {
    return {
      currentDressId: null,
    };
  },

  components: {
    Dress,
    ExitCross
},

  computed: {
    ...mapState("dressingUpMenu", ["dresses"]),
    ...mapGetters("localization", ["loc"]),
  },

  methods: {
    setCurrentDressId: function (value) {
      this.currentDressId = value;
      window.mp.trigger("frac:setCurrDress", value);
    },
    toApply: function () {
      window.mp.trigger("frac:applyDress", this.currentDressId);
    },
    toCancel: function () {
      window.mp.trigger("frac:cancelDress");
    },
  },
};
</script>

<style lang="scss" scoped>
.dressing-up-menu {
  display: flex;
  align-items: flex-end;
  justify-content: flex-start;
  width: 100%;
  height: 100%;
  position: absolute;
  left: 0;
  top: 0;
  background: linear-gradient(86.86deg, #000000 2.01%, rgba(0, 0, 0, 0) 97.7%);
  font-family: "Akrobat";

  .exit-cross {
    position: absolute;
    top: 2.105rem;
    right: 2.105rem;
  }
  & > button {
    cursor: pointer;
    position: absolute;
    top: 2.105rem;
    right: 2.105rem;
    width: 2.632rem;
    height: 2.632rem;
    border: none;
    background: none;
    outline: none;

    img {
      width: 100%;
      height: 100%;
    }
  }

  .dressing-up-menu_wrap {
    display: flex;
    flex-flow: column;
    justify-content: flex-end;
    position: relative;
    padding-bottom: 3.737rem;
    padding-left: 6.105rem;

    & > div:not(.list) {
      padding-left: 0.789rem;
    }

    .title {
      display: block;
      font-weight: 700;
      font-size: 2.526rem;
      line-height: 3.053rem;
      text-transform: uppercase;
      color: #ffffff;
      max-width: 15rem;
      margin-bottom: 1.579rem;
      padding-left: 1.842rem !important;
    }

    /* .descr {
      font-size: 0.8rem;
      line-height: 1rem;
      color: #ffffff;
    } */
    .list {
      height: 30.421rem;
      overflow-y: scroll;
      display: flex;
      flex-flow: column;
      justify-content: flex-start;
      transform-origin: center;
      transform: scaleX(-1);
      padding-right: 0.789rem;
      margin-right: -0.789rem; //5px scroll bar

      & > div {
        transform: scaleX(-1);
      }

      &::-webkit-scrollbar {
        width: 0.263rem;
      }
      &::-webkit-scrollbar-thumb {
        background: #301934 ;
      }
    }
    .btns-wrap {
      margin-top: 1.053rem;
      width: 100%;
      display: flex;
      flex-direction: column;

      .btn {
        cursor: pointer;
        display: flex;
        align-items: center;
        justify-content: center;
        height: 3.947rem;
        min-height: 3.947rem;
        border: 0.053rem solid transparent;
        background: linear-gradient(180deg, #465970 0%, #1c2228 100%);
        transition: all 0.3s ease;
        font-weight: 700;
        font-size: 1.263rem;
        line-height: 1.526rem;
        text-transform: uppercase;
        color: #ffffff;
        outline: none;

        &:hover {
          background: linear-gradient(180deg, #301934  0%, #591b87 100%);
          /* background: linear-gradient(
            180deg,
            rgba(71, 44, 132, 0.25) 0%,
            rgba(75, 0, 130, 0.25) 100%
          );
          border: 1px solid #301934 ;
          box-shadow: inset 0px 0px 0.789rem rgba(75, 0, 130, 0.86); */
        }

        &:last-child {
          margin-top: 0.526rem;
        }
      }
    }
  }
}
</style>
