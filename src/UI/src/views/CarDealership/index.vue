<template>
  <div class="avtobuy">
    <div class="main-stat-block">
      <div class="text-block">
        <div class="text-block-pos">
          <div class="text-block-name">
            <div class="text-block-name-title">Cars Showroom</div>
            <div class="text-block-name-subtitle">{{ type }}</div>
          </div>
          <div class="text-block-stat">
            <div class="car-data">
              <div class="car-data-box">
                <div class="car-data-box-img">
                  <img src="./Assets/benz.svg" alt="" />
                </div>
                <div class="car-data-box-text">
                  <div class="car-data-box-text-number">
                    {{ fuelConsumption }}
                  </div>
                  <div class="car-data-box-text-text">consumption</div>
                </div>
              </div>
              <div class="car-data-box">
                <div class="car-data-box-img">
                  <img src="./Assets/bak.svg" alt="" />
                </div>
                <div class="car-data-box-text">
                  <div class="car-data-box-text-number">
                    {{ fuelTank }} liters
                  </div>
                  <div class="car-data-box-text-text">The capacity of the tank</div>
                </div>
              </div>
              <div class="car-data-box">
                <div class="car-data-box-img">
                  <img src="./Assets/ves.svg" alt="" />
                </div>
                <div class="car-data-box-text">
                  <div class="car-data-box-text-number">{{ trunk }} kg</div>
                  <div class="car-data-box-text-text">
            The capacity of the trunk
                  </div>
                </div>
              </div>
            </div>
            <div class="car-parametr">
              <div class="car-parametr-box">
                <div class="car-parametr-box-title">speed</div>
                <div class="car-parametr-box-number-block">
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.speed.value >= 1"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.speed.value >= 2"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.speed.value >= 3"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.speed.value >= 4"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.speed.value >= 5"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                </div>
              </div>
              <div class="car-parametr-box">
                <div class="car-parametr-box-title">Brake</div>
                <div class="car-parametr-box-number-block">
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.braking.value >= 1"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.braking.value >= 2"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.braking.value >= 3"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.braking.value >= 4"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.braking.value >= 5"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                </div>
              </div>
              <div class="car-parametr-box">
                <div class="car-parametr-box-title">acceleration</div>
                <div class="car-parametr-box-number-block">
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.acceleration.value >= 1"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.acceleration.value >= 2"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.acceleration.value >= 3"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.acceleration.value >= 4"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.acceleration.value >= 5"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                </div>
              </div>
              <div class="car-parametr-box">
                <div class="car-parametr-box-title">management</div>
                <div class="car-parametr-box-number-block">
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.traction.value >= 1"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.traction.value >= 2"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.traction.value >= 3"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.traction.value >= 4"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                  <div class="car-parametr-box-number">
                    <div
                      v-if="specifications.traction.value >= 5"
                      class="car-parametr-box-number-content"
                    ></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="color-block">
        <div class="color-block-pos">
          <div
            class="color-box"
            :class="{ ActiveColor: currentColor == index }"
            v-for="(Color, index) in colors"
            :key="index"
            @click="setCurrentColor(index)"
          >
            <div
              :style="{
                'background-color':
                  'rgb(' + Color.r + ',' + Color.g + ',' + Color.b + ')',
              }"
              class="color-box-content"
            ></div>
          </div>
        </div>
      </div>
      <div class="buy-block">
        <div class="buy-block-pos">
          <div class="test-drive" @click="testDrive">
            <span>Test drive</span>
          </div>
          <div
            class="nal"
            v-on:click="ActiveBuyNal"
            v-bind:class="{ 'buy-active': isActiveBuyNal }"
          >
            <img src="./Assets/nal.svg" alt="" />
            <div class="card-text">Cash</div>
          </div>
          <div
            class="card"
            v-on:click="ActiveBuyCard"
            v-bind:class="{ 'buy-active': isActiveBuyCard }"
          >
            <img src="./Assets/card.svg" alt="" />
            <div class="card-text">Card</div>
          </div>
          <div
            class="familyon"
            v-on:click="ActiveBuyFON"
            v-bind:class="{ 'buy-active': isActiveBuyFON }"
          >
            <img src="./Assets/nal.svg" alt="" />
            <div class="card-text">Private</div>
          </div>
          <div
            class="familyoff"
            v-on:click="ActiveBuyFOFF"
            v-bind:class="{ 'buy-active': isActiveBuyFOFF }"
          >
            <img src="./Assets/card.svg" alt="" />
            <div class="card-text">Family</div>
          </div>
          <div class="buy" @click="buyVehicle"><span>Buy</span></div>
        </div>
      </div>
    </div>
    <div class="car-list" @mousewheel="scrollcarlist">
      <div
        v-on:click="selectcar(index)"
        class="car-list-box"
        :class="{ ActiveCar: isActiveCar == index }"
        v-for="(Cars, index) in cars"
        :key="index"
      >
        <div class="car-list-box-pos">
          <div class="car-list-box-title">{{ Cars.name }}</div>
          <div class="car-list-box-price">${{ Cars.price }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState, mapMutations } from "vuex";

export default {
  name: "AvtoBuy",
  data() {
    return {
      Car: this.cars,
      isActiveBuyNal: true,
      isActiveBuyCard: false,
      isActiveBuyFON: true,
      isActiveBuyFOFF: false,
      isActiveCar: 0,
      currentColor: 0,
    };
  },
  computed: {
    ...mapState("carDealership", [
      "nameModel",
      "price",
      "colors",
      "specifications",
      "maxValueSpecifications",
      "fuelConsumption",
      "fuelTank",
      "trunk",
      "soldOut",
      "cars",
      "type"
    ]),
    ...mapGetters("localization", ["loc"]),
  },
  mounted(){
    // using for test
    // setTimeout(() => this.setDealerName('myName'), 4000)
  },
  methods: {
    ...mapMutations('carDealership', ['setDealerType']),
    selectcar(index) {
      if (this.isActiveCar == index) return;
      window.mp.trigger("carDealership:selectVeh", index);
      this.isActiveCar = index;
    },
    setCurrentColor: function (color) {
      window.mp.trigger(
        "carDealership:setColor",
        this.colors[color].r,
        this.colors[color].g,
        this.colors[color].b
      );
      this.currentColor = color;
    },
    ActiveBuyNal() {
      this.isActiveBuyNal = true;
      this.isActiveBuyCard = false;
    },
    ActiveBuyCard() {
      this.isActiveBuyNal = false;
      this.isActiveBuyCard = true;
    },
    ActiveBuyFON() {
      this.isActiveBuyFON = true;
      this.isActiveBuyFOFF = false;
    },
    ActiveBuyFOFF() {
      this.isActiveBuyFON = false;
      this.isActiveBuyFOFF = true;
    },
    scrollcarlist(e) {
      e = window.event || e;
      var delta = Math.max(-1, Math.min(1, e.wheelDelta || -e.detail));
      this.$el.querySelector(".car-list").scrollLeft -= delta * 40; // Multiplied by 40
      e.preventDefault();
    },
    testDrive: function () {
      window.mp.trigger("carDealership:testDrive", this.nameModel);
    },
    buyVehicle: function () {
      if (this.isActiveBuyNal) {
        window.mp.trigger("carDealership:buyVehicleCash", !this.isActiveBuyFON);
      } else {
        window.mp.trigger("carDealership:buyVehicleCard", !this.isActiveBuyFON);
      }
    },
  },
};
</script>
<style lang="scss">
$width: 1920;
$height: 1080;

@function conv($px, $direction: false) {
  @if $direction {
    @return ($px / $height) * 100vh;
  } @else {
    @return ($px / $width) * 100vw;
  }
}

.avtobuy {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: radial-gradient(
    50% 50% at 50% 50%,
    rgba(23, 23, 28, 0) 0%,
    #17171c 100%
  );
  display: flex;
  flex-direction: column;
  gap: 1vw;
}

.main-stat-block {
  justify-content: space-between;
  display: flex;
  height: 85%;
}

.car-list {
  overflow: hidden;
  overflow-x: scroll;
  white-space: nowrap;
  padding-bottom: 1vw;
}

.car-list::-webkit-scrollbar {
  height: 0.3vw;
}

.car-list::-webkit-scrollbar-track {
  background: rgba(0, 0, 0, 0.25);
}
.car-list::-webkit-scrollbar-thumb {
  background-color: #301934 ;
}

.car-list-box:first-child {
  margin: 0;
}

.car-list-box-pos {
  height: 100%;
}

.car-list-box-title {
  font-family: "Akrobat";
  font-style: normal;
  font-weight: 700;
  font-size: 2.2222vh;
  text-transform: uppercase;
  color: #ffffff;
}

.car-list-box-price {
  margin-top: 0.7407vh;
  display: table;
  padding: 0.9259vh 0.6771vw;

  font-family: "Akrobat";
  font-style: normal;
  font-weight: 700;
  text-transform: uppercase;
  font-size: 1.4815vh;

  color: #ffffff;
  background: rgba(255, 255, 255, 0.05);
  border: 0.0547vw solid;
  border-image-source: linear-gradient(
    90deg,
    rgba(255, 255, 255, 0.09) 0%,
    rgba(255, 255, 255, 0) 101.25%
  );

  /* font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    text-transform: uppercase;
    
    display: flex;
    align-items: center;
    justify-content: flex-start;

    color: #FFFFFF;
    background: rgba(255, 255, 255, 0.05);
    border: 0.0547vw solid;
    border-image-source: linear-gradient(90deg, rgba(255, 255, 255, 0.09) 0%, rgba(255, 255, 255, 0) 101.25%); */
}

.color-block {
  display: flex;
  align-items: flex-end;
}

.color-block-pos {
  display: flex;
  gap: 0.5208vw;
}

.color-box {
  width: 1.8229vw;
  height: 1.8229vw;
  background: rgba(0, 0, 0, 0.25);
  border-radius: 2.2917vw;
  display: flex;
  align-items: center;
  justify-content: center;
}

.ActiveColor {
  background: linear-gradient(
    180deg,
    rgba(71, 44, 132, 0.5) 0%,
    rgba(75, 0, 130, 0.5) 100%
  );
}

.color-box-content {
  background: #ffffff;
  width: 0.9115vw;
  height: 0.9115vw;
  border-radius: 2.2917vw;
}

.buy-block {
  display: flex;
  align-items: flex-end;
}

.text-block-stat {
  display: flex;
  flex-direction: column;
  gap: 2.7778vh;
}

.text-block-pos {
  display: flex;
  flex-direction: column;
  gap: 14.8148vh;
}

.text-block-name-title {
  font-family: "Akrobat";
  font-style: normal;
  font-weight: 800;
  font-size: conv(48, true);

  background: linear-gradient(90deg, #301934  0%, #591b87 100%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
  text-fill-color: transparent;

  text-shadow: 0vh 0vw 5.8594vw rgba(255, 255, 255, 0.25);
}

.text-block-name-subtitle {
  font-family: "Akrobat";
  font-style: normal;
  font-weight: 600;
  font-size: conv(20, true);

  text-transform: uppercase;

  color: #ffffff;
}

.car-data {
  display: flex;
  flex-direction: column;
  gap: 0.9259vh;
}

.car-data-box {
  display: flex;
  align-items: center;
  gap: 0.9259vh;
}

.car-data-box-img {
  width: conv(60, true);
  height: conv(60, true);
  background: linear-gradient(
    180deg,
    rgba(0, 0, 0, 0.5) 0%,
    rgba(0, 0, 0, 0) 100%
  );
  display: flex;
  align-items: center;
  justify-content: center;
}

.car-data-box-img img {
  width: conv(25, true);
}

.car-data-box-text-number {
  font-family: "Akrobat";
  font-style: normal;
  font-weight: 700;
  font-size: conv(20, true);
  /* identical to box height */

  text-transform: uppercase;

  color: #ff8383;
}

.car-data-box-text-text {
  font-family: "Akrobat";
  font-style: normal;
  font-weight: 700;
  font-size: conv(24, true);
  /* identical to box height */

  text-transform: uppercase;

  color: #ffffff;
}

.car-parametr {
  display: flex;
  flex-direction: column;
  gap: 0.463vh;
}

.car-parametr-box {
  display: flex;
  flex-direction: column;
  gap: conv(7.68, true);
  background: linear-gradient(
    90deg,
    rgba(0, 0, 0, 0.5) 0%,
    rgba(0, 0, 0, 0) 100%
  );
  padding: conv(4.99968, true) 0 conv(14.016, true) 1.042vw;
}

.car-parametr-box-title {
  font-family: "Akrobat";
  font-style: normal;
  font-weight: 700;
  font-size: conv(24, true);
  line-height: conv(29, true);
  /* identical to box height */

  text-transform: uppercase;

  color: #ffffff;
}

.car-parametr-box-number {
  display: flex;
  align-items: center;
  justify-content: center;
  background: rgba(255, 255, 255, 0.05);
  width: conv(19, true);
  height: conv(19, true);
}

.car-parametr-box-number-block {
  display: flex;
  gap: 1.1979vw;
}

.car-parametr-box-number-content {
  width: conv(10.98, true);
  height: conv(10.98, true);
  background: #301934 ;
  box-shadow: 0vw 0vh 0.762vw rgba(71, 44, 132, 0.55);
}

.buy-block-pos {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-rows: 1fr 1fr 1fr;
  gap: 0.5vw;
  grid-template-areas:
    "test-drive test-drive"
    "nal card"
    "familyon familyoff"
    "buy buy";
}
.test-drive {
  grid-area: test-drive;
  background: rgba(58, 58, 58, 0.25);
  font-family: "Akrobat";
  font-style: normal;
  font-weight: 700;
  font-size: 1.25vw;
  display: flex;
  align-items: center;
  justify-content: center;
  text-transform: uppercase;
  cursor: pointer;
  color: #ffffff;
}

.familyon {
  grid-area: familyon;
  background: rgba(0, 0, 0, 0.25);
  /* background: #301934 ; */
  display: flex;
  align-items: center;
  flex-direction: column;
  justify-content: center;
  cursor: pointer;
  gap: conv(9.6, true);
}

.familyon > img {
  width: conv(30, true);
  height: conv(30, true);
}

.familyoff {
  grid-area: familyoff;
  background: rgba(0, 0, 0, 0.25);
  /* background: #301934 ; */
  display: flex;
  align-items: center;
  flex-direction: column;
  justify-content: center;
  cursor: pointer;
  gap: conv(9.6, true);
}

.familyoff > img {
  width: conv(30, true);
  height: conv(30, true);
}

.nal {
  grid-area: nal;
  background: rgba(0, 0, 0, 0.25);
  /* background: #301934 ; */
  display: flex;
  align-items: center;
  flex-direction: column;
  justify-content: center;
  cursor: pointer;
  gap: conv(9.6, true);
}

.nal > img {
  width: conv(30, true);
  height: conv(30, true);
}

.card {
  grid-area: card;
  background: rgba(0, 0, 0, 0.25);
  display: flex;
  align-items: center;
  flex-direction: column;
  justify-content: center;
  gap: conv(9.6, true);
  cursor: pointer;
}

.card > img {
  width: conv(30, true);
  height: conv(30, true);
}

.card-text {
  font-family: "Akrobat";
  font-style: normal;
  font-weight: 700;
  text-transform: uppercase;

  color: #ffffff;
}

.buy-active {
  background: linear-gradient(
    180deg,
    rgba(71, 44, 132, 0.5) 0%,
    rgba(75, 0, 130, 0.5) 100%
  );
}

.buy {
  grid-area: buy;
  background: rgba(0, 0, 0, 0.25);
  //background: linear-gradient(180deg, #301934  0%, #591b87 100%);
  font-family: "Akrobat";
  font-style: normal;
  font-weight: 700;
  line-height: 1.8125rem;
  color: #ffffff;
  display: flex;
  align-items: center;
  justify-content: center;
  text-transform: uppercase;
  cursor: pointer;
  transition: 0.3s ease;
  position: relative;

  span {
    position: relative;
    z-index: 2;
  }

  &::after {
    content: "";
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: linear-gradient(180deg, #301934  0%, #591b87 100%);
    opacity: 0;
    z-index: 1;
    transition: 0.3s ease;
  }

  &:hover {
    &::after {
      opacity: 1;
    }
  }
}

.car-list-box {
  display: inline-block;
  background: rgba(0, 0, 0, 0.25);
  width: 15.3646vw;
  height: 10.3704vh;
  margin-left: 0.5208vw;
  padding: 2.1296vh 0vw 2.1296vh 1.0417vw;
  position: relative;

  &::after {
    content: "";
    z-index: 2;
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    pointer-events: none;
    opacity: 0;
    transition: 0.2s ease;
    background: linear-gradient(0deg, #444444 0%, #3a3a3a 100%);
  }

  img,
  span,
  div,
  button {
    z-index: 3;
    position: relative;
  }

  &:not(.ActiveCar):hover {
    &::after {
      opacity: 1;
    }
  }
}

.avtobuy {
  padding: 2.4074vh 2.6042vw 2.4074vh 2.6042vw;
}

.test-drive {
  width: 15.3646vw;
  height: conv(74.98, true);
}

.nal {
  width: 7.5521vw;
  height: conv(74.98, true);
}

.card {
  width: 7.5521vw;
  height: conv(74.98, true);
}

.buy {
  width: 15.3646vw;
  height: conv(74.98, true);
}

.ActiveCar {
  background: linear-gradient(
    180deg,
    rgba(71, 44, 132, 0.5) 0%,
    rgba(75, 0, 130, 0.5) 100%
  );
}

.card-text {
  font-size: 1.6667vh;
}

.buy {
  font-size: 2.2222vh;
}

.test-drive {
  font-size: 2.2222vh;
  cursor: pointer;
  transition: 0.3s ease;
  position: relative;

  span {
    position: relative;
    z-index: 2;
  }

  &::after {
    content: "";
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: linear-gradient(0deg, #444444 0%, #3a3a3a 100%);
    opacity: 0;
    z-index: 1;
    transition: 0.3s ease;
    pointer-events: none;
  }

  &:hover {
    &::after {
      opacity: 1;
    }
  }
}
</style>
