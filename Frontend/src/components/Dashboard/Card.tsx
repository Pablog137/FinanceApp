import "../../styles/components/Dasboard/Card.css";

export default function Card() {
  return (
    <div className="bg-blue-400 col-span-10 rounded-2xl p-5 flex flex-col gap-2">
      <h4 className="text-2xl font-semibold">$5,823.000</h4>
      <h5 className="text-xl">2956 4761 9365 2341</h5>
      <div className="flex justify-start gap-6">
        <p>
          Valid From <span className="font-semibold">10/25</span>
        </p>
        <p>
          Valid Thru <span className="font-semibold">10/30</span>
        </p>
      </div>

      <h5>Card Holder</h5>
      <div className="flex justify-between">
        <h4 className="text-xl">Anna Kapustina</h4>
        <i className="mastercard-icon"></i>
      </div>
    </div>
  );
}
