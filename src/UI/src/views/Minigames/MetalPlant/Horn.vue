<template>
  <div class="mplant-horn" @click="click">
    <div
      class="mplant-horn_line"
      :style="{
        width: furnaceTempearature + '%',
      }"
    ></div>
    <svg
      width="52"
      height="52"
      viewBox="0 0 52 52"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      class="mplant-horn_img"
    >
      <path
        d="M41.363 24.2011L37.0466 16.725C36.933 16.5282 36.7696 16.3647 36.5727 16.251C36.3759 16.1374 36.1526 16.0775 35.9254 16.0775C35.6981 16.0775 35.4748 16.1374 35.278 16.251C35.0811 16.3647 34.9177 16.5282 34.8041 16.725L30.4877 24.2011C30.374 24.398 30.3142 24.6213 30.3142 24.8486C30.3142 25.0759 30.374 25.2991 30.4876 25.496C30.6013 25.6928 30.7647 25.8563 30.9615 25.97C31.1584 26.0836 31.3817 26.1435 31.6089 26.1435H34.6304V34.636C34.6304 34.9794 34.7669 35.3088 35.0097 35.5516C35.2525 35.7945 35.5819 35.9309 35.9254 35.9309C36.2688 35.9309 36.5982 35.7945 36.841 35.5516C37.0838 35.3088 37.2203 34.9794 37.2203 34.636V26.1435H40.2418C40.469 26.1435 40.6923 26.0836 40.8892 25.97C41.086 25.8563 41.2494 25.6928 41.3631 25.496C41.4767 25.2991 41.5365 25.0759 41.5365 24.8486C41.5365 24.6213 41.4767 24.398 41.363 24.2011Z"
        :fill="active ? 'rgba(255, 223, 109, 1)' : 'white'"
      />
      <path
        d="M24.2755 31.197V10.4609C24.2755 9.0872 23.7298 7.76972 22.7584 6.79835C21.787 5.82697 20.4696 5.28125 19.0958 5.28125C17.7221 5.28125 16.4046 5.82697 15.4332 6.79835C14.4619 7.76972 13.9161 9.0872 13.9161 10.4609V31.197C12.4666 32.2841 11.3959 33.7997 10.8557 35.5292C10.3154 37.2586 10.3331 39.1142 10.906 40.8331C11.479 42.552 12.5783 44.0471 14.0481 45.1065C15.518 46.1659 17.2839 46.736 19.0958 46.736C20.9077 46.736 22.6736 46.1659 24.1435 45.1065C25.6134 44.0471 26.7127 42.552 27.2856 40.8331C27.8586 39.1142 27.8762 37.2586 27.336 35.5292C26.7957 33.7997 25.725 32.2841 24.2755 31.197ZM19.0958 41.5391C18.2948 41.5401 17.5184 41.2622 16.9 40.7532C16.2815 40.2442 15.8594 39.5358 15.7063 38.7496C15.5531 37.9634 15.6784 37.1484 16.0606 36.4444C16.4428 35.7405 17.0581 35.1915 17.8009 34.8918V14.8378C17.8009 14.4944 17.9373 14.165 18.1802 13.9222C18.423 13.6793 18.7524 13.5429 19.0958 13.5429C19.4393 13.5429 19.7686 13.6793 20.0115 13.9222C20.2543 14.165 20.3907 14.4944 20.3907 14.8378V34.8918C21.1336 35.1915 21.7489 35.7405 22.1311 36.4444C22.5133 37.1484 22.6386 37.9634 22.4854 38.7496C22.3322 39.5358 21.9102 40.2442 21.2917 40.7532C20.6732 41.2622 19.8968 41.5401 19.0958 41.5391Z"
        :fill="active ? 'rgba(255, 223, 109, 1)' : 'white'"
      />
    </svg>
    <div class="mplant-horn_text">
      {{
        !firstActive
          ? "Click to warm up the pitch"
          : active
          ? "The stove is gaining temperature..."
          : "The stove loses the temperature "
      }}
    </div>
  </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
export default {
  data() {
    return {
      radius: 85,
      active: false,
      firstActive: false,
      timer: null,
    };
  },
  computed: {
    ...mapGetters("gameMetalPlant", ["furnaceTempearature"]),
    dashArray() {
      const plength = this.percentLength * this.furnaceTempearature;
      return `${plength} ${this.circleLength - plength}`;
    },
    // dashOffset(){

    // },
    circleLength() {
      return 2 * Math.PI * this.radius;
    },
    percentLength() {
      return this.circleLength / 100;
    },
  },
  methods: {
    ...mapActions("gameMetalPlant", ["hornAction"]),
    click() {
      this.hornAction();
      if (this.furnaceTempearature === 0) return;

      if (!this.firstActive) this.firstActive = true;

      if (this.active) {
        clearTimeout(this.timer);
      } else {
        this.active = true;
      }

      this.timer = setTimeout(() => {
        this.active = false;
      }, 500);
    },
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.mplant-horn {
  width: 100%;
  height: conv(97);
  display: flex;
  align-items: center;
  position: relative;
  background: rgba(255, 255, 255, 0.05);
  padding-left: conv(40);
  cursor: pointer;

  div,
  svg {
    z-index: 3;
    position: relative;
  }

  &_line {
    z-index: 2 !important;
    position: absolute !important;
    top: 0;
    left: 0;
    height: 100%;
    background: linear-gradient(
      270deg,
      rgba(103, 12, 16, 0.8) 0%,
      rgba(75, 0, 130, 0.8) 100%
    );
    transition: all 0.5s linear;
  }

  &_text {
    font-weight: 700;
    font-size: conv(20);
    line-height: conv(24);
    text-transform: uppercase;
    color: #ffffff;
    opacity: 0.4;
  }

  &_img {
    width: conv(52);
    height: conv(52);
    margin-right: conv(40);
    transition: 0.1s ease;
  }
}
</style>