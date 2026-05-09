<template>
  <div class="robshop" v-show="show">
    <div class="robshop_title">
      <div>
        {{ loc("robshop:title") }}:
        <div>
          <svg
            width="18"
            height="17"
            viewBox="0 0 18 17"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
            v-for="(elem, index) in 5"
            :key="index"
          >
            <path
              d="M8.95283 0.891885C9.10251 0.43123 9.75421 0.43123 9.90389 0.891885L11.5612 5.9927C11.6282 6.19871 11.8202 6.33819 12.0368 6.33819H17.4001C17.8844 6.33819 18.0858 6.958 17.694 7.2427L13.355 10.3952C13.1797 10.5225 13.1064 10.7482 13.1733 10.9542L14.8307 16.055C14.9804 16.5157 14.4531 16.8987 14.0613 16.614L9.72225 13.4615C9.54701 13.3342 9.30971 13.3342 9.13447 13.4615L4.79546 16.614C4.4036 16.8987 3.87636 16.5157 4.02604 16.055L5.68339 10.9542C5.75033 10.7482 5.677 10.5225 5.50175 10.3952L1.16274 7.2427C0.770887 6.958 0.972275 6.33819 1.45664 6.33819H6.81995C7.03656 6.33819 7.22854 6.19871 7.29548 5.9927L8.95283 0.891885Z"
              :fill="level > index ? '#FE2A2A' : 'rgba(255, 255, 255, 0.07)'"
            />
          </svg>
        </div>
      </div>
      <div>
        <span>{{ loc("robshop:info:money") }}</span>
        <span>{{ money.toLocaleString() }}$</span>
      </div>
    </div>

    <Progress :progress="progress" />
    <Info />
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";

import Progress from "./Progress.vue";
import Info from "./Info.vue";

export default {
  computed: {
    ...mapState("robberyShop", ["show", "progress", "level", "money"]),
    ...mapGetters("localization", ["loc"]),
  },
  components: {
    Progress,
    Info,
  },
};
</script>


<style lang="scss">
.robshop {
  position: absolute;
  right: 1.053rem;
  bottom: 1.053rem;
  background: linear-gradient(
    180deg,
    rgba(12, 14, 20, 0.7) 0%,
    rgba(12, 14, 20, 0) 100%
  );
  width: 19rem;
  height: 9.368rem;
  color: #fff;
  text-transform: uppercase;
  font-family: "Akrobat";
  padding: 0.632rem 1rem 0.526rem 1.158rem;

  &_title {
    display: flex;
    justify-content: space-between;
    font-weight: 700;
    font-size: 0.842rem;
    line-height: 1rem;
    margin-bottom: 0.368rem;

    & > div {
      display: flex;
      flex-direction: column;

      &:last-child {
        align-items: flex-end;

        span:last-child {
          color: #7df299;
          font-weight: 700;
          font-size: 1.053rem;
          line-height: 1.263rem;
        }
      }

      div {
        display: grid;
        grid-template-columns: repeat(5, 1fr);
        width: fit-content;
        margin-top: 0.128rem;
        column-gap: 0.105rem;

        svg {
          transform: rotate(16.5deg);
          width: 1.053rem;
          height: 1.053rem;
        }
      }
    }
  }


  &_star {
    margin-right: 0.2rem;
    margin-top: -0.1rem;
    width: 1rem;
    height: 1rem;
    &:first-child {
      margin-left: 0.5rem;
    }
  }
}
</style>