<template>
  <div class="farm-house_profile">
    <div class="farm-house_profile-items">
      <div
        class="farm-house_profile-item"
        v-for="item in levelsList"
        :key="item.requiredLevel"
        :class="{ locked: item.requiredLevel > userLevel.level }"
      >
        <div class="farm-house_profile-item_img">
          <img :src="`/img/farmHouse/${item.plantHoles[0].img}.png`" alt="" />
        </div>
        <div>
          <div class="farm-house_profile-item_lvl">
            <div>{{ item.requiredLevel }}</div>
            <div>level</div>
          </div>
          <div class="farm-house_profile-item_info">
            <div>{{ loc(item.plantHoles[0].title) }}</div>
            <div>{{ item.plantHoles[0].time }}</div>
            <div>+{{ item.plantHoles[0].exp }}&nbsp;Exp</div>
            <div>+{{ item.plantHoles[0].fetus }}&nbsp;To the harvest</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";

export default {
  computed: {
    ...mapState("farmHouse", ["levelsList"]),
    ...mapGetters("farmHouse", ["userLevel"]),
    ...mapGetters("localization", ["loc"]),
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.farm-house_profile {
  background: linear-gradient(
    252.44deg,
    rgba(19, 20, 21, 0.99) 0%,
    rgba(31, 34, 37, 0.99) 100%
  );
  padding: conv(46) conv(21) conv(40) conv(37);

  &-items {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: conv(10);
    width: 100%;
    max-height: conv(654);
    overflow-y: auto;
    padding-right: conv(10);

    &::-webkit-scrollbar {
      background: rgba(255, 255, 255, 0.05);
      width: conv(5);

      &-thumb {
        background: #301934 ;
      }
    }
  }

  &-item {
    width: 100%;
    height: 100%;
    min-height: conv(322);
    max-height: conv(322);
    background: rgba(255, 255, 255, 0.03);
    padding: conv(40) conv(21) conv(59) conv(24);
    display: flex;

    &_img {
      width: conv(125);
      height: 100%;
      position: relative;

      img {
        position: absolute;
        left: 0;
        width: 100%;
        bottom: conv(27);
        height: auto;
      }
    }

    & > div:last-child {
      margin-left: conv(25);
      width: conv(185);
      display: flex;
      flex-direction: column;
    }

    &_lvl {
      width: conv(176);
      display: flex;
      align-items: center;
      position: relative;
      height: conv(56);

      div {
        &:first-child {
          position: absolute;
          top: 50%;
          right: 100%;
          transform: translate(100%, -50%);
          width: conv(56);
          height: conv(56);
          display: flex;
          justify-content: center;
          align-items: center;
          font-weight: 800;
          font-size: conv(20);
          line-height: conv(24);
          text-align: center;
          text-transform: uppercase;
          color: #1a1717;
          background: url(/img/farmHouse/star.png) center center no-repeat;
          background-size: contain;
        }

        &:last-child {
          font-weight: 700;
          font-size: conv(32);
          line-height: conv(38);
          text-transform: uppercase;
          color: #ffffff;
          padding-left: conv(65);
        }
      }
    }

    &_info {
      margin-top: conv(38);
      display: flex;
      flex-direction: column;

      div {
        width: 100%;

        &:not(:last-child) {
          margin-bottom: conv(10);
        }

        &:first-child {
          font-weight: 700;
          font-size: conv(24);
          line-height: conv(29);
          text-transform: uppercase;
          color: #ffffff;
          margin-bottom: conv(8);
        }

        &:nth-child(2),
        &:nth-child(3),
        &:last-child {
          font-weight: 500;
          font-size: conv(20);
          line-height: conv(24);
          text-transform: uppercase;
          color: #ffffff;
        }
      }
    }

    &.locked {
      opacity: 0.07;
    }
  }
}
</style>