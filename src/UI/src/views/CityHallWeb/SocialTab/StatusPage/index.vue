<template>
  <div class="page">
    <div class="page__title">{{ loc("cityHallWeb_121") }}</div>
    <div class="list">
      <div class="current-name">
        <div>Your name</div>
        <div>{{ name }}</div>
      </div>
      <div class="current-id">
        <div>Your ID</div>
        <div>{{ cardId }}</div>
      </div>
    </div>
    <div class="status-list">
      <div
        class="status-list__block"
        v-for="block in socialStatus"
        :key="block.key"
      >
        <div class="page__desc">
          <img src="/img/cityHallWeb/social/i.svg" alt="" />
          <div>{{ loc(block.desc) }}</div>
        </div>
        <div class="text">
          <div
            class="text__value"
            v-for="(value, index) in block.values"
            :key="value.key"
          >
             <span>{{ loc(value) }}</span><span v-if="block.values.length !== index + 1">,</span> 
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";
export default {
  name: "StatusPage",

  computed: {
    ...mapGetters("localization", ["loc"]),
    ...mapState("cityHallWeb", ["cardId", "name", "socialStatus"]),
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.city-hall-web {
  .body-tab {
    .list {
      display: grid;
      grid-template-columns: conv(439) 1fr;
      margin-bottom: conv(32);
      margin-top: conv(35);

      & > div {
        display: flex;
        flex-direction: column;

        & > div {
          text-transform: uppercase;

          &:first-child {
            font-weight: 600;
            font-size: conv(20);
            line-height: conv(24);
            color: rgba(255, 255, 255, 0.5);
            margin-bottom: conv(3);
          }

          &:last-child {
            font-weight: 700;
            font-size: conv(32);
            line-height: conv(38);
            color: #ffffff;
          }
        }
      }
    }

    .status-list {
      display: flex;
      flex-flow: column;
      padding-right: 0;
      margin-right: 0;
      overflow-y: auto;
      max-height: conv(308);

      &::-webkit-scrollbar {
        background: rgba(255, 255, 255, 0.05);
        width: conv(5);

        &-thumb {
          background: #301934 ;
        }
      }

      &__block {
        width: 100%;
        // height: conv(149);
        // min-height: conv(149);
        background: rgba(0, 0, 0, 0.2);
        display: flex;
        flex-direction: column;
        padding: conv(34) conv(76) conv(37);

        &:not(:last-child) {
          margin-bottom: conv(10);
        }

        .page__desc {
          font-weight: 700;
          font-size: conv(32);
          line-height: conv(38);
          text-transform: uppercase;
          color: #ffffff;
          padding: 0;
          margin-bottom: conv(11);
          position: relative;

          img {
            position: absolute;
            top: 50%;
            left: conv(-12);
            transform: translate(-100%, -50%);
            width: conv(24);
            height: conv(24);
          }

          &::before {
            display: none;
          }
        }

        .text {
          max-width: 100%;
          display: flex;
          flex-direction: column;
          font-weight: 500;
          font-size: cnv(24);
          line-height: conv(29);
          text-transform: uppercase;
          color: #ffffff;
          gap: 0.1vh;

          &__value {
            font-weight: 500;
            font-size: cnv(24);
            line-height: conv(29);
            text-transform: uppercase;
            color: #ffffff;
          }
        }
      }
    }
  }
}
</style>
