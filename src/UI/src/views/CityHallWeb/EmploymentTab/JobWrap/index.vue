<template>
  <div class="job-wrap">
    <div
      class="job-wrap__img"
      :style="{
        backgroundImage:
          'url(/img/optionsMenu/informationTab/works/' +
          selectedJob.img +
          '.png)',
      }"
    ></div>
    <div>
      <div class="job-wrap__name">{{ loc(selectedJob.title) }}</div>
      <div class="job-wrap__level">
        {{ selectedJob.entryLevel }} {{ loc("cityHallWeb_28") }}
      </div>

      <div class="job-wrap__desc">{{ loc(selectedJob.description) }}</div>
      <div class="page__btn btn" @click="getDirection">
        <div>
          <img src="/img/cityHallWeb/jobs/map.png" alt="" />
        </div>
        <div>{{ loc("cityHallWeb_27") }}</div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "JobWrap",

  props: {
    selectedJob: Object,
  },
  computed: {
    ...mapGetters("localization", ["loc"]),
  },

  methods: {
    getDirection: function () {
      window.mp.triggerServer("mmenu:job:waypoint", this.selectedJob.point);
    },
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.job-wrap {
  display: flex;
  flex-direction: column;
  width: 100%;
  height: 100%;
  position: relative;

  &__img {
    width: 100%;
    height: conv(198);
    background-size: cover;
    background-repeat: no-repeat;
    background-position: center;
  }

  & > div:last-child {
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
    background: rgba(0, 0, 0, 0.1);
    padding: conv(33) conv(280) 0 conv(38);
    position: relative;
  }

  &__name {
    font-weight: 800;
    font-size: conv(36);
    line-height: conv(43);
    text-transform: uppercase;
    color: #ffffff;
    margin-bottom: conv(22);
  }

  &__level {
    height: conv(34);
    background: rgba(255, 255, 255, 0.04);
    border-radius: conv(25);
    font-weight: 700;
    font-size: conv(14);
    line-height: conv(17);
    display: flex;
    align-items: center;
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
    padding: 0 conv(25);
    width: fit-content;
    margin-bottom: conv(25);
  }

  &__desc {
    width: 100%;
    font-weight: 500;
    font-size: conv(20);
    line-height: conv(24);
    text-transform: uppercase;
    color: #ffffff;
  }

  .page__btn {
    position: absolute;
    top: conv(33);
    right: conv(40);
    display: flex;
    align-items: center;
    justify-content: flex-start;
    height: conv(70) !important;
    width: conv(349);
    background: rgba(255, 255, 255, 0.04) !important;
    padding: 0 !important;
    cursor: pointer;
    transition: 0.2s ease;

    &:hover {
      background: rgba(255, 255, 255, 0.1) !important;
    }

    & > div {
      &:first-child {
        display: flex;
        align-items: center;
        justify-content: center;
        height: 100%;
        width: conv(70);
        min-width: conv(70);
        background: rgba(255, 255, 255, 0.05);

        img {
          width: 100%;
          height: 100%;
        }
      }

      &:last-child {
        font-weight: 600;
        font-size: conv(24);
        line-height: conv(29);
        text-transform: uppercase;
        color: #ffffff;
        padding-left: conv(17);
      }
    }
  }
}
</style>
