<template>
  <div class="farm-house_help">
    <div class="farm-house_help-wrap">
      <button class="farm-house_help-back" @click="$emit('back')">
        <div><img src="/img/inputMenu/arrow.svg" alt="" /></div>
     back
      </button>
      <div class="farm-house_help-title">Help</div>
      <div class="farm-house_help-desc">
    Gardening for beginners! <br/>
We have prepared a small "cheat sheet", the purpose of which is to help the gardener who takes the first steps to navigate in the most important summer huts and outside.
You have to start planning: Decide what and where you will plant. <br/>
We buy seeds, fertilizers and irrigation so that our seeds quickly get the first fruits.And of course a box for our harvest. <br/>
Then go water and fill our irrigation. <br/>
Now you have to decide exactly where you want to carry out horticulture.I would advise you not to run and take the free beds that are directly opposite my house. <br/>
Don't forget to pour and fertilize your seeds.If you cannot pour them within a minute, it will be browned and disappeared. <br/>
If you do everything right, you can pick up your work after 3 minutes and sell the buyer you can find on the card. <br/>
      </div>
      <div class="farm-house_help-info">
        <div v-for="(elem, index) in helpList" :key="index">
          <div>-</div>
          {{ elem }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  computed: {
    ...mapState("farmHouse", ["helpList"]),
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.farm-house_help {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 100;
  display: flex;
  justify-content: center;
  align-items: flex-start;
  padding-top: conv(103);
  background: rgba(7, 7, 7, 0.97);

  &-wrap {
    display: flex;
    flex-direction: column;
    width: conv(1018);
  }

  &-back {
    text-transform: uppercase;
    height: conv(48);
    width: fit-content;
    padding-right: conv(15);
    display: flex;
    align-items: center;
    font-weight: 700;
    font-size: conv(20);
    line-height: conv(24);
    color: #ffffff;
    cursor: pointer;
    transition: 0.2s ease;
    background: rgba(255, 255, 255, 0);

    div {
      width: conv(47);
      min-width: conv(47);
      height: 100%;
      display: flex;
      align-items: center;
      justify-content: center;
      background: rgba(255, 255, 255, 0.07);
      margin-right: conv(14);

      img {
        height: conv(14);
      }
    }

    &:hover {
      div {
        background: rgba(255, 255, 255, 0.15);
      }
    }
  }

  &-title {
    margin: conv(16) 0 conv(6);
    font-weight: 700;
    font-size: conv(48);
    line-height: conv(58);
    text-transform: uppercase;
    color: #ffffff;
  }

  &-desc {
    margin-bottom: conv(33);
    font-weight: 700;
    font-size: conv(20);
    line-height: conv(24);
    text-transform: uppercase;
    color: #ffffff;
  }

  &-info {
    width: 100%;
    height: conv(504);
    max-height: conv(504);
    padding: conv(25) conv(20);
    overflow-y: auto;
    display: flex;
    flex-direction: column;

    &::-webkit-scrollbar {
      background: rgba(255, 255, 255, 0.05);
      width: conv(5);

      &-thumb {
        background: #301934 ;
      }
    }

    & > div {
      display: flex;
      align-items: flex-start;
      font-weight: 700;
      font-size: conv(20);
      line-height: conv(24);
      color: #ffffff;

      &:not(:last-child) {
        margin-bottom: conv(13);
      }

      & > div {
        margin-right: conv(15);
      }
    }
  }
}
</style>