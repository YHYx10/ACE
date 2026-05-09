<template>
  <div class="bank-org bank-family">
    <div class="bank-org_info">
      <div class="bank-org_header">
        <div
          class="bank-org_header-item"
          v-for="(elem, index) in ['Family name', 'Family balance']"
          :key="index"
        >
          <div>{{ elem }}</div>
          <div>
            {{
              index === 0
                ? familyName
                : `$${familyBalance.toLocaleString("ru")}`
            }}
          </div>
        </div>
      </div>
      <div class="bank-org_btns">
        <button
          class="bank-main_services-item"
          v-for="(elem, index) in [
            ['Fill out the remaining amount', 'TopUp', 'topup'],
            ['Widthdraw', 'Withdraw', 'widthdraw'],
            ['Bonuses pay', 'Bonus', 'transfer'],
            ['Zahlung zu Hause ', 'PayForHouse', 'transfer'],
          ]"
          :key="index"
          @click="() => setModal(elem[1])"
        >
          <img :src="`/img/bank/main/${elem[2]}.svg`" alt="" />
          <div class="bank-main_services-text">{{ elem[0] }}</div>
        </button>
      </div>

      <div class="bank-org_income">
        <div class="bank-org_income-diagram">
          <div>
            <div>Work</div>
            <div>
              +&nbsp;${{ calcSum(workProfitArray).toLocaleString("ru") }}
            </div>
          </div>
          <div>
            <!-- round -->
            <svg viewBox="0 0 140 140" xmlns="http://www.w3.org/2000/svg">
              <circle
                cx="70"
                cy="70"
                r="58"
                stroke="#FFCA42"
                stroke-width="22"
                fill="none"
                :stroke-dasharray="2 * Math.PI * 58"
                :stroke-dashoffset="0"
              />
              <circle
                cx="70"
                cy="70"
                r="58"
                stroke="#78FFBE"
                stroke-width="22"
                fill="none"
                :stroke-dashoffset="
                  2 * Math.PI * 58 -
                  ((2 * Math.PI * 58) / 100) *
                    ((calcSum(workProfitArray) /
                      (calcSum(workProfitArray) + calcSum(otherProfitArray))) *
                      100)
                "
                :stroke-dasharray="2 * Math.PI * 58"
              />
            </svg>
          </div>
          <div>
            <div>General</div>
            <div>
              +&nbsp;${{ calcSum(otherProfitArray).toLocaleString("ru") }}
            </div>
          </div>
        </div>
        <div class="bank-org_income-info">
          <div>Family income</div>
          <div>
            +&nbsp;${{
              (
                calcSum(workProfitArray) + calcSum(otherProfitArray)
              ).toLocaleString("ru")
            }}
          </div>
        </div>
      </div>
    </div>

    <div class="bank-org_transfer">
      <div class="bank-main_title">Your transactions</div>

      <div class="bank-main_transaction-wrap">
        <div
          class="bank-main_transaction-item"
          v-for="(item, index) in getTransfers"
          :key="index"
        >
          <div :class="item.directFrom ? 'low' : 'high'">
            <img
              :src="`/img/bank/icon/${item.directFrom ? 'low' : 'high'}.png`"
              alt=""
            />
          </div>
          <div>{{ loc(item.comment) }}</div>
          <div>{{item.directFrom ? '-' : '+'}}&nbsp;${{ getAmount(item) }}</div>
        </div>
      </div>
    </div>

    <transition name="bank-modal">
      <component
        v-if="currentModal"
        :is="currentModal"
        @closeModal="closeModal"
      />
    </transition>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";
import PayForHouse from "./modals/BankFamilyPayForHouse";
export default {
  name: "BankFamily",
  components: {
    PayForHouse,
  },
  data() {
    return {
      currentCharity: null,
      currentModal: null,
    };
  },
  computed: {
    ...mapState("bank/family", [
      "familyName",
      "transfersList",
      "familyBalance",
      "staffList",
    ]),
    ...mapGetters("localization", ["loc"]),
    calcCircumference: function () {
      let number = 2 * Math.PI * 119;
      return number;
    },
    getTransfers() {
      return this.transfersList.slice().sort((a, b) => b.date - a.date);
    },
    workProfitArray() {
      return this.transfersList.filter(
        (element) => !element.directFrom && element.comment == "Money_Work"
      );
    },
    otherProfitArray() {
      return this.transfersList.filter(
        (element) => !element.directFrom && element.comment != "Money_Work"
      );
    },
    circleValue: function () {
      let workProfit = this.calcSum(this.workProfitArray);
      let totalProfit = this.calcSum(this.otherProfitArray) + workProfit;
      const maxCircleValue = this.calcCircumference;
      const maxCircleValuePer = (maxCircleValue / totalProfit) * workProfit;
      return maxCircleValue - maxCircleValuePer;
    },
  },
  methods: {
    setModal(name) {
      if (name === "PayForHouse") {
        this.setCurrentModal("PayForHouse");
      } else if (name === "Bonus") {
        this.$emit("setCurrentModal", {
          component: "Bonus",
          dataModal: {
            type: "family",
            staffList: this.staffList,
          },
        });
      } else {
        this.$emit("setCurrentModal", {
          component: name,
          dataModal: {
            type: "family",
            balance: this.familyBalance,
          },
        });
      }
    },
    calcSum(arr) {
      let sum = 0;
      arr.forEach(function (item) {
        sum += item.value - item.tax;
      });
      return sum;
    },
    setCurrentModal(value) {
      this.currentModal = value;
    },
    closeModal() {
      this.currentModal = null;
    },
    getIcon(transact) {
      if (!transact.directFrom) return "replenishment-icon";
      else return "withdrawal-icon";
      // bonus{
      //   background-image: url('/img/bank/bonus-icon.png');
      // }
      // withdrawal{
      //   background-image: url('/img/bank/withdrawal-icon.png');
      // }
      // tax{
      //   background-image: url('/img/bank/suitcase.png');
      // }
    },
    getAmount(transact) {
      if (transact.directFrom) return transact.value.toLocaleString("ru");
      else return (transact.value - transact.tax).toLocaleString("ru");
    },
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @if $direction {
    @return ($px / 20) + rem;
  } @else {
    @return ($px / 20) + rem;
  }
}

.bank-family{
  padding-bottom: conv(22, true);
}
</style>
