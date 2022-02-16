namespace Namespace
{
    using System.Linq;
    using System.Collections.Generic;
    using System;

    public static class Module
    {
        public static object fence(object lst, object numrails)
        {
            var fence = (from _ in Enumerable.Range(0, numrails)
                select (new List<object>
                {
                    null
                } * lst.Count)).ToList();
            var rails = Enumerable.Range(0, numrails - 1).ToList() + Enumerable
                .Range(0, Convert.ToInt32(Math.Ceiling(Convert.ToDouble(0 - (numrails - 1)) / -1)))
                .Select(_x_1 => numrails - 1 + _x_1 * -1).ToList();
            foreach (var _tup_1 in lst.Select((_p_1, _p_2) => Tuple.Create(_p_2, _p_1)))
            {
                var n = _tup_1.Item1;
                var x = _tup_1.Item2;
                fence[rails[n % rails.Count]][n] = x;
            }

            return (from rail in fence
                from c in rail
                where c != null
                select c).ToList();
        }

        public static object encode(object text, object n)
        {
            if (!(n is int))
            {
                throw ValueError("Invalid key");
            }

            if (!(text is str))
            {
                throw ValueError("Invalid text");
            }

            return "".join(fence(text, n));
        }

        public static object decode(object text, object n)
        {
            if (!(n is int))
            {
                throw ValueError("Invalid key");
            }

            if (!(text is str))
            {
                throw ValueError("Invalid text");
            }

            var rng = Enumerable.Range(0, text.Count);
            var pos = fence(rng, n);
            return "".join(from n in rng
                select text[pos.index(n)]);
        }

        public static object z = encode("lol kek cheburek", 4);

        public static object y = decode(z, 3);
    }
}
