<template>
  <div class="autoschool-main">
    <div class="autoschool-title">
      <span>CENTER</span>
   Licensing
    </div>
    <div class="autoschool-main_wrap">
      <div class="autoschool-main_vehicle">
        <div
          class="autoschool-main_item"
          v-for="(elem, index) in vehicle"
          :key="index"
        >
          <div class="autoschool-main_item-header">
            {{ elem.title }}
          </div>
          <div class="autoschool-main_item-img">
            <img :src="`/img/autoSchool/vehicle/${elem.key}.png`" alt="" />
          </div>
          <div class="autoschool-main_item-btn">
            <button
              class="bank-btn action"
              v-if="!licensesList[elem.keys].status"
              @click="() => $emit('select-exam', index, elem.keys)"
  >
Go to the examination
            </button>
            <button
              class="buy"
              v-if="!licensesList[elem.keys].status"
              @click="() => buy(elem.keys)"
            >
              <div>BUY</div>
              <div
                >$ {{ elem.price.toLocaleString("ru-RU") }}</div
              >
            </button>
            <button
              class="have"
              v-if="licensesList[elem.keys].status"
              disabled="true"
            >
     You have a license
            </button>
          </div>
        </div>
      </div>
      <div class="autoschool-main_lic">
        <div
          class="autoschool-main_lic-items"
          v-for="(elem, idx) in [
            'Licenses for work',
            'Additional licenses',
          ]"
          :key="idx + 'wrap'"
        >
          <div class="autoschool-main_lic-header">{{ elem }}</div>
          <div class="autoschool-main_lic-list">
            <div
              class="autoschool-main_lic-item"
              v-for="(element, index) in idx === 0 ? licWork : licDop"
              :key="index"
            >
              <div>{{ element.name }}</div>
              <button
                v-if="!licensesList[element.lkey].status"
                class="buy"
                @click="() => buy(element.lkey)"
              >
                <div>BUY</div>
                <div>$ {{ element.price.toLocaleString("ru-RU") }}</div>
              </button>
              <button class="have" v-if="licensesList[element.lkey].status">
              You have a license
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  computed: {
    ...mapState("autoSchool", ["licVehicle", "licWork", "licDop", "licensesList"]),
  },
  data() {
    return {
      vehicle: [
        {
          keys: 0,
          key: "bike",
          title: "Bike License",
          price: 25000,
        },
        {
          keys: 1,
          key: "auto",
          title: "Car License",
          price: 35000,
        },
        {
          keys: 2,
          key: "truck",
          title: "Truck License",
          price: 45000,
        },
        {
          keys: 3,
          key: "boat",
          title: "Boat License",
          price: 55000,
        },
        {
          keys: 4,
          key: "helicopter",
          title: "Helicopter License",
          price: 65000,
        },
        {
          keys: 5,
          key: "airplane",
          title: "Airplane License",
          price: 75000,
        },
      ],
    };
  },
  methods: {
    buy(elem) {
      window.mp.trigger("school:buyLic", JSON.stringify(elem));
    },
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.autoschool {
  &-main {
    padding-top: conv(63);
    display: flex;
    flex-direction: column;
    align-items: center;

    .buy {
      color: #ffffff;
      font-weight: 700;
      font-size: conv(16);
      line-height: conv(19);
      justify-content: space-between;
      padding: 0 conv(28);
      background: rgba(255, 255, 255, 0.05);
      position: relative;

      div:last-child {
        color: #a0ff98;
      }

      div {
        z-index: 10;
        position: relative;
      }

      &::after {
        content: "";
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        opacity: 0;
        background: linear-gradient(
          180deg,
          #56bf4d 0%,
          #335530 100%,
          #335530 100%
        );
        transition: 0.3s ease;
        z-index: 6;
      }

      &:hover {
        &::after {
          opacity: 1;
        }

        div:last-child {
          color: white;
        }
      }
    }

    .have {
      display: flex;
      align-items: center;
      justify-content: center;
      text-transform: uppercase;
      white-space: nowrap;
      color: #ffffff;
      font-weight: 700;
      font-size: conv(16);
      line-height: conv(19);
      background: rgba(160, 255, 152, 0.35);
      cursor: auto;
    }

    .autoschool-title {
      margin-bottom: conv(62, true);
    }

    &_wrap {
      display: grid;
      grid-template-columns: conv(790) conv(480);
      column-gap: conv(80);
    }

    &_vehicle {
      display: grid;
      grid-template-columns: repeat(3, conv(250));
      grid-template-rows: repeat(2, conv(389));
      column-gap: conv(20);
      row-gap: conv(20);
      width: 100%;
    }

    &_item {
      display: flex;
      flex-direction: column;
      width: 100%;
      height: 100%;
      background: linear-gradient(
        0deg,
        rgba(255, 255, 255, 0.05) 0%,
        rgba(255, 255, 255, 0) 100%
      );
      padding-top: conv(3);
      position: relative;

      &:first-child &-img {
        margin-top: conv(22);
        height: conv(117);
      }

      &:nth-child(2) &-img {
        margin-top: conv(30);
        height: conv(99);
      }

      &:nth-child(3) &-img {
        margin-top: conv(16);
        height: conv(117);
      }

      &:nth-child(4) &-img {
        margin-top: conv(32);
        height: conv(90);
      }

      &:nth-child(5) &-img {
        margin-top: conv(28);
        height: conv(96);
      }

      &:last-child &-img {
        margin-top: conv(30);
        height: conv(100);
      }

      &-img {
        display: flex;
        justify-content: center;
        align-items: flex-start;
        height: 100%;
      }

      &-header {
        width: 100%;
        display: flex;
        align-items: center;
        justify-content: center;
        height: conv(66);
        background: rgba(255, 255, 255, 0.05);
        font-weight: 700;
        font-size: conv(20);
        line-height: conv(24);
        display: flex;
        align-items: center;
        text-align: center;
        color: #ffffff;
      }

      &-btn {
        position: absolute;
        left: 50%;
        transform: translateX(-50%);
        top: conv(216);
        width: conv(190);

        button {
          width: 100%;
          height: conv(65);
          display: flex;
          justify-content: center;
          align-items: center;
          cursor: pointer;
          font-weight: 700;
          font-size: conv(16);
          line-height: conv(19);
          display: flex;
          align-items: center;
          text-align: center;
          text-transform: uppercase;
          color: #ffffff;
        }

        .action {
          background: linear-gradient(180deg, #301934  0%, #591b87 100%);
          margin-bottom: conv(10);
          background: linear-gradient(
            180deg,
            rgba(71, 44, 132, 0.25) 0%,
            rgba(75, 0, 130, 0.25) 100%
          );
          outline: 1px solid #301934 ;
          box-shadow: inset 0px 0px 0.789rem rgba(75, 0, 130, 0.86);
          &:hover {
            box-shadow: inset 0px 0px 3.789rem rgba(75, 0, 130, 0.86);
            background: linear-gradient(
              180deg,
              rgba(71, 44, 132, 0.35) 0%,
              rgba(75, 0, 130, 0.35) 100%
            );
          }
        }
      }
    }

    &_lic {
      width: 100%;
      max-height: conv(798);
      display: flex;
      flex-direction: column;
      overflow-x: hidden;
      overflow-y: auto;

      &::-webkit-scrollbar {
        width: conv(3);
      }
      &::-webkit-scrollbar-track {
        background: rgba(255, 255, 255, 0.23);
      }
      &::-webkit-scrollbar-thumb {
        background: #ff0000;
      }

      &-items {
        width: 100%;
        display: flex;
        flex-direction: column;

        &:first-child {
          margin-bottom: conv(19);
        }
      }

      &-header {
        width: 100%;
        background: linear-gradient(
          90deg,
          rgba(255, 255, 255, 0.05) 0%,
          rgba(255, 255, 255, 0) 100%
        );
        padding-left: conv(20);
        display: flex;
        align-items: center;
        font-weight: 700;
        font-size: conv(20);
        line-height: conv(24);
        color: #ffffff;
        margin-bottom: conv(10);
        height: conv(66);
      }

      &-list {
        display: flex;
        flex-direction: column;
        width: 100%;
      }

      &-item {
        width: 100%;
        height: conv(86);
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding: conv(10) conv(20);
        border: 1.15px solid rgba(255, 255, 255, 0.09);

        & > div {
          font-weight: 700;
          font-size: conv(20);
          line-height: conv(24);
          color: #ffffff;
        }

        button {
          display: flex;
          align-items: center;
          justify-content: space-between;
          width: conv(190);
          height: 100%;
          padding: 0 conv(28);
          background: rgba(255, 255, 255, 0.05);

          div {
            font-weight: 700;
            text-transform: uppercase;

            &:first-child {
              font-size: conv(16);
              line-height: conv(19);
              color: #ffffff;
            }

            &:last-child {
              font-size: conv(16);
              line-height: conv(19);
              color: #a0ff98;
            }
          }
        }

        &:not(:last-child) {
          margin-bottom: conv(5);
        }
      }
    }
  }
}
</style>